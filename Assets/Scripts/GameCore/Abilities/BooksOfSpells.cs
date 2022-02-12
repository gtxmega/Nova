using System;
using System.Collections.Generic;
using System.Linq;
using GameCore.Attributes.Expirience;
using UnityEngine;

namespace GameCore.Abilities
{
    [Serializable]
    public class BooksOfSpells
    {
        public event Action OnOpenBook;
        public event Action OnCloseBook;
        public event Action<Ability> AbilityLevelUp;

        public Ability booksAbility;

        public bool IsOpenBook { get; private set; }
        public KeyCode HotkeyOpenBook => _hotkeyOpenBook;
        public KeyCode HotkeyCloseBook => _hotkeyCloseBook;

        [Header("Hotkeys")]
        [SerializeField] private KeyCode _hotkeyOpenBook;
        [SerializeField] private KeyCode _hotkeyCloseBook;
        
        [SerializeField] private List<Ability> _abilities = new List<Ability>();

        private IExpirience _expirience;


        public void InitBooks(IExpirience expirience)
        {
            _expirience = expirience;
            _expirience.OnLevelUp += UnitLevelUp;

            booksAbility.SetAbilityLevel(_expirience.AbilityPoints);

            foreach (var ability in _abilities)
            {
                ability.InitAbility();
            }
        }

        public void OpenBook()
        {
            IsOpenBook = true;
            OnOpenBook?.Invoke();
        }

        public void CloseBook()
        {
            IsOpenBook = false;
            OnCloseBook?.Invoke();
        }
        
        
        public void WaiteHotkey()
        {
            foreach (var ability in _abilities)
            {
                if (Input.GetKeyDown(ability.HotKey))
                {
                    if (_expirience.AbilityPoints > 0)
                    {
                        _expirience.ChangeAbilityPoints(-1);
                        booksAbility.SetAbilityLevel(_expirience.AbilityPoints);
                        
                        AbilityLevelUp?.Invoke(ability);
                    }
                }
            }
        }

        public void AbilityAddLevel(int slotID)
        {
            if (_expirience.AbilityPoints > 0)
            {
                _expirience.ChangeAbilityPoints(-1);
                booksAbility.SetAbilityLevel(_expirience.AbilityPoints);

                var ability = _abilities.First(f => f.SlotID == slotID);
                AbilityLevelUp?.Invoke(ability);

                if (ability.Level >= ability.MaxLevel)
                    _abilities.Remove(ability);
            }
        }

        public Ability[] GetAbilities()
        {
            return _abilities.ToArray();
        }

        private void UnitLevelUp(int level)
        {
            booksAbility.SetAbilityLevel(_expirience.AbilityPoints);
        }
        
    }
}
