using System;
using UnityEngine;

using GameCore.Abilities;

namespace GameCore.UI.Panels.Abilities
{
    public class AbilitiesPanel : MonoBehaviour
    {
        [Header("UI Sprites")] 
        [SerializeField] private Sprite _abilityLevelUpSprite;
        
        [Header("UI ability")]
        [SerializeField] private GameObject _abilitiesPanel;
        [SerializeField] private AbilitiesSlot[] _firstAbilitiesSlots;
        [SerializeField] private AbilitiesSlot[] _secondAbilitiesSlots;
        [SerializeField] private AbilitiesSlot[] _mainAbilitiesSlots;
        
        [Header("UI The books of spells")]
        [SerializeField] private GameObject _booksOfSpellsPanel;
        [SerializeField] private AbilitiesSlot[] _booksFirstSlots;
        [SerializeField] private AbilitiesSlot[] _booksSecondSlots;
        [SerializeField] private AbilitiesSlot[] _booksThreeSlots;
        

        private readonly int _countAbilitySlotsInRow = 4;

        private IAbilityDisplying _abilityDisplying;
        private BooksOfSpells _booksOfSpells;
        
        private void Awake()
        {
            for (int i = 0; i < _countAbilitySlotsInRow; ++i)
            {
                _firstAbilitiesSlots[i].SetLevelUpImage(_abilityLevelUpSprite);
                _secondAbilitiesSlots[i].SetLevelUpImage(_abilityLevelUpSprite);
                _mainAbilitiesSlots[i].SetLevelUpImage(_abilityLevelUpSprite);
            }
        }

        public void ShowMainAbilitiesSlots(IAbilityDisplying abilityDisplying)
        {
            _abilityDisplying = abilityDisplying;
            var abilities = _abilityDisplying.GetMainAbilities();
            
            _abilitiesPanel.SetActive(true);
            
            for (int i = 0; i < abilities.Length; ++i)
            {
                for (int j = 0; j < _mainAbilitiesSlots.Length; ++j)
                {
                    if (abilities[i].SlotID == j)
                    {
                        _mainAbilitiesSlots[j].InitSlot(abilities[i]);
                        _mainAbilitiesSlots[j].EventSlotClick += _mainAbilitiesSlots[j].ClickAbilityButton;
                    }
                }
            }
        }

        public void ShowBooksOfSpells(BooksOfSpells booksOfSpells)
        {
            _booksOfSpells = booksOfSpells;
            
            for (int i = 0; i < _secondAbilitiesSlots.Length; ++i)
            {
                if (booksOfSpells.booksAbility.SlotID == i)
                {
                    _secondAbilitiesSlots[i].InitSlot(booksOfSpells.booksAbility);
                    _secondAbilitiesSlots[i].EventSlotClick += OpenBooksOfSpells;
                }
            }
            
        }

        private void OpenBooksOfSpells(int slotID)
        {
            _abilitiesPanel.SetActive(false);
            EnableDisableBooksOfSpells(true);

            var abilitites = _booksOfSpells.GetAbilities();

            for (int i = 0; i < abilitites.Length; ++i)
            {
                for (int j = 0; j < _booksFirstSlots.Length; ++j)
                {
                    if (abilitites[i].SlotID == j)
                    {
                        _booksFirstSlots[j].InitSlot(abilitites[i]);
                        _booksFirstSlots[j].EventSlotClick += OnClickAbilityBooksOfSpells;
                    }
                }
            }
            
        }

        private void OnClickAbilityBooksOfSpells(int slotID)
        {
            _booksOfSpells.AbilityAddLevel(slotID);
            EnableDisableBooksOfSpells(false);
            
            ShowMainAbilitiesSlots(_abilityDisplying);
        }

        private void EnableDisableBooksOfSpells(bool isActive)
        {
            _booksOfSpellsPanel.SetActive(isActive);
        }

        public void HideAllAbilitiesSlots()
        {
            foreach (var slots in _mainAbilitiesSlots)
            {
                slots.ClearSlot();
            }
        }

        public void HideBooksOfSpells()
        {
            EnableDisableBooksOfSpells(false);
            foreach (var slot in _secondAbilitiesSlots)
            {
                slot.ClearSlot();
            }
        }
    }
}
