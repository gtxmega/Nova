using System;
using System.Collections;
using System.Collections.Generic;
using GameCore.Actors;
using GameCore.Attributes.Mana;
using GameCore.UI.Panels.Abilities;
using UnityEngine;
using Zenject;

namespace GameCore.Abilities
{
    public abstract class Ability : MonoBehaviour, IAbility
    {
        public event Action<Ability> TryActivateAbility;
        public event Action<float, float> AbilityCooldownTick;
        public event Action OnAbilityLevelUp;
        public KeyCode HotKey => _hotKey;
        public int SlotID => _slotID;
        public int MaxLevel => _maxLevel;
        public int Level => _level;

        public EAbilityStatus AbilityStatus => _abilityStatus;
        public bool IsCooldown => _isCooldown;
        
        
        [Header("Description")]
        [SerializeField] protected AbilityDescription _abilityDescription;
        [SerializeField] protected EAbilitySlotType _abilitySlotType;

        [Header("Level")] 
        [SerializeField] protected int _maxLevel;
        [SerializeField] protected int _level;

        [SerializeField] protected KeyCode _hotKey;
        [SerializeField] protected int _slotID;
        [SerializeField] protected bool _needConfirm;

        [Header("Graphic")] 
        [SerializeField] protected GameObject _vfxInitAbility;
        [SerializeField] protected GameObject _vfxApplayingAbility;

        protected EAbilityStatus _abilityStatus;
        
        private bool _isCooldown;
        private Coroutine _coroutineCooldown;
        
        [Inject] protected IMana _manaAttributes { get; private set; }

        public virtual void InitAbility()
        {
            
        }
        
        public virtual EAbilityStatus TryApplyingAbility()
        {
            TryActivateAbility?.Invoke(this);
            
            return _abilityStatus;
        }

        public virtual EAbilityStatus ConfirmAbility(Vector3 point, Actor _target)
        {
            return EAbilityStatus.Successfully;
        }

        public virtual void ActivatePassiveAbility()
        {
            
        }

        public virtual void DeactivatePassiveAbility()
        {
            
        }

        public virtual void CancelAbility()
        {
            
        }
        
        public virtual void AbilityLevelUp()
        {
            _level++;
            OnAbilityLevelUp?.Invoke();
        }

        public virtual void SetAbilityLevel(int level)
        {
            _level = level;
            OnAbilityLevelUp?.Invoke();
        }

        protected virtual void StartCooldown(float cooldownTime)
        {
            _isCooldown = true;

            if (_coroutineCooldown == null)
                _coroutineCooldown = StartCoroutine(AbilityRecovery(cooldownTime));
        }
        
        private IEnumerator AbilityRecovery(float cooldownTime)
        {
            var timer = cooldownTime;
            
            while (timer >= 0.0f)
            {
                timer -= Time.deltaTime;
                AbilityCooldownTick?.Invoke(timer, cooldownTime);
                
                yield return null;
            }

            _coroutineCooldown = null;
            _isCooldown = false;
        }
        
        
        public AbilityDescription GetAbilityDescription()
        {
            return _abilityDescription;
        }

        public EAbilitySlotType GetAbilitySlotType()
        {
            return _abilitySlotType;
        }
        
        protected bool CheckMana(float neededMana)
        {
            var totalMana = _manaAttributes.CurrentMana - neededMana;
            return totalMana >= 0.0f;
        }
        
    }
}
