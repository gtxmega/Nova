using System;
using System.Collections;
using UnityEngine;

using GameCore.Abilities;
using GameCore.Actors;
using GameCore.Attributes.Mana;
using Unity.Mathematics;
using UnityEngine.VFX;
using Zenject;

namespace Game.Abilities
{
    public class SunStrikeAbility : Ability
    {
        [Header("Attributes per Level")]
        [SerializeField] private BaseAbilityLevel[] _abilityLevels;

        [SerializeField] private LayerMask _groundLayer;
        
        private float _damage;
        private float _usageDistance;
        private float _usageArea;
        private float _cooldown;
        private float _manaCost;

        private Transform _vfxInitAbilityTemp;
        private Transform _vfxApplayingAbilityTemp;

        [Inject] private Camera _rayCastCamera;

        private Coroutine _coroutineShowVFX;
        

        public override void InitAbility()
        {
            SetLevelAttributes(0);
        }

        public override void AbilityLevelUp()
        {
            if(_level >= _abilityLevels.Length) return;
            
            base.AbilityLevelUp();

            SetLevelAttributes(_level);
        }
        

        public override EAbilityStatus TryApplyingAbility()
        {
            if (IsCooldown) return _abilityStatus = EAbilityStatus.Cooldown;
            
            if (CheckMana(_manaCost))
            {
                if (_needConfirm)
                {
                    _abilityStatus = EAbilityStatus.NeedToConfirm;
                    
                    if (_coroutineShowVFX == null)
                        _coroutineShowVFX = StartCoroutine(ShowInitVFX());

                    base.TryApplyingAbility();
                    return _abilityStatus;
                }

                base.TryApplyingAbility();
                return _abilityStatus = EAbilityStatus.Successfully;
            }
            else
            {
                base.TryApplyingAbility();
                return _abilityStatus = EAbilityStatus.NotEnoughMana;
            }
            
        }

        public override EAbilityStatus ConfirmAbility(Vector3 point, Actor _target)
        {
            if (CheckMana(_manaCost))
            {
                _manaAttributes.TryChangeCurrentMana(-_manaCost);
                ShowApplayingVFX(point);
                StartCooldown(_cooldown);
                
                return EAbilityStatus.Successfully;
            }
            else
            {
                return EAbilityStatus.NotEnoughMana;
            }
        }

        public override void CancelAbility()
        {
            _abilityStatus = EAbilityStatus.Ready;

            if (_coroutineShowVFX != null)
            {
                StopCoroutine(_coroutineShowVFX);
                _coroutineShowVFX = null;
            }

            if(_vfxInitAbilityTemp != null)
                _vfxInitAbilityTemp.gameObject.SetActive(false);
        }


        private IEnumerator ShowInitVFX()
        {
            if (_vfxInitAbilityTemp == null)
            {
                _vfxInitAbilityTemp = Instantiate(_vfxInitAbility.transform, Vector3.zero, quaternion.identity);
                _vfxInitAbilityTemp.gameObject.SetActive(false);
            }

            SetVFXToCorsorPosition();
            _vfxInitAbilityTemp.gameObject.SetActive(true);

            while (_abilityStatus == EAbilityStatus.NeedToConfirm)
            {
                SetVFXToCorsorPosition();
                yield return null;
            }

            _coroutineShowVFX = null;
        }

        private void ShowApplayingVFX(Vector3 point)
        {
            if (_vfxApplayingAbilityTemp == null)
            {
                var vfx = Instantiate(_vfxApplayingAbility, point, Quaternion.identity);
                //vfx.GetComponent<VisualEffect>().SendEvent("OnPlay");
            }
        }

        private void SetVFXToCorsorPosition()
        {
            var ray = _rayCastCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitResult, 150.0f, _groundLayer))
            {
                _vfxInitAbilityTemp.position = hitResult.point;
            }
        }

        private void SetLevelAttributes(int level)
        {
            _damage = _abilityLevels[level].Damage;
            _usageDistance = _abilityLevels[level].UsageDistance;
            _usageArea = _abilityLevels[level].UsageArea;
            _cooldown = _abilityLevels[level].Cooldown;
            _manaCost = _abilityLevels[level].ManaCost;
        }
    }
}
