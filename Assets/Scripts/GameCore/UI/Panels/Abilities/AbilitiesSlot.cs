using System;
using GameCore.Abilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameCore.UI.Panels.Abilities
{
    public class AbilitiesSlot : MonoBehaviour, IPointerClickHandler
    {
        public event Action<int> EventSlotClick;

        public bool IsEmptySlot { get; private set; }
        
        [SerializeField] private Image _slotImage;
        [SerializeField] private Image _levelUpImage;
        [SerializeField] private Image _cooldownImage;
        [SerializeField] private Text _cooldownText;
        [SerializeField] private Text _abilityLevelText;

        [Header("UI")] 
        [SerializeField] private Sprite _slotHover;
        
        private Ability _ability;

        public void InitSlot(Ability ability)
        {
            _ability = ability;

            var abilityDescription = _ability.GetAbilityDescription();
            
            SetSlotImage(abilityDescription.AbilitySprite);
            SetAbilityLevelText();
            
            SubscribeEvents();

            EnableDisableSlot(true);
        }
        
        private void SetSlotImage(Sprite slotSprite)
        {
            _slotImage.sprite = slotSprite;
        }

        public void SetLevelUpImage(Sprite levelUpSprite)
        {
            _levelUpImage.sprite = levelUpSprite;
        }

        private void SetFillAmountCooldown(float currentTime, float maxTime)
        {
            if(_cooldownImage.gameObject.activeSelf == false)
                EnableDisableCooldownSlot(true);
            
            _cooldownImage.fillAmount = currentTime / maxTime;
            SetCooldownText(currentTime);
            
            if(currentTime <= 0.0f)
                EnableDisableCooldownSlot(false);
        }

        private void SetCooldownText(float cooldownTime)
        {
            _cooldownText.text = Mathf.Round(cooldownTime).ToString();
        }

        private void SetAbilityLevelText()
        {
            var level = _ability.Level;
            
            if (level < 0) level = 0;
            
            _abilityLevelText.text = level.ToString();
        }

        private void EnableDisableCooldownSlot(bool isEnable)
        {
            _cooldownImage.gameObject.SetActive(isEnable);
            _cooldownText.gameObject.SetActive(isEnable);
        }

        public void ClearSlot()
        {
            
            _abilityLevelText.text = "";
            _cooldownText.text = "";

            EventSlotClick = null;

            EnableDisableSlot(false);
            UnsubscribeEvents();
        }

        private void EnableDisableSlot(bool isEnable)
        {
            gameObject.SetActive(isEnable);
        }

        private void SubscribeEvents()
        {
            if (_ability != null)
            {
                _ability.AbilityCooldownTick += SetFillAmountCooldown;
                _ability.OnAbilityLevelUp += SetAbilityLevelText;
            }
        }

        private void UnsubscribeEvents()
        {
            if (_ability != null)
            {
                _ability.AbilityCooldownTick -= SetFillAmountCooldown;
                _ability.OnAbilityLevelUp -= SetAbilityLevelText;
            }
        }

        public void ClickAbilityButton(int slotID)
        {
            _ability.TryApplyingAbility();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            EventSlotClick?.Invoke(_ability.SlotID);
        }
    }
}
