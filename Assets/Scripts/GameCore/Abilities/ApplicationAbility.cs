using System.Collections.Generic;
using System.Linq;
using GameCore.Actors;
using GameCore.Attributes.Expirience;
using UnityEngine;
using Zenject;


namespace GameCore.Abilities
{
    public class ApplicationAbility : MonoBehaviour, IApplicationAbility, IAbilityDisplying
    {
        private delegate void WaiteHotkeys();

        private WaiteHotkeys _waiteHotkeys;
        

        [Header("The books of spells")] 
        [SerializeField] private BooksOfSpells _booksOfSpells;
        
        [SerializeField] private List<Ability> _mainAbilities = new List<Ability>();
        
        [Header("Other abilities")]
        [SerializeField] private Ability[] _firstAbilities;
        [SerializeField] private Ability[] _secondAbilities;

        [SerializeField] private KeyCode _cancelAbilityKey;
        
        private Ability _applyingAbility;

        [Inject] private IExpirience _expirience;

        private void Awake()
        {
            _booksOfSpells.OnOpenBook += OpenBooksOfSpells;
            _booksOfSpells.OnCloseBook += CloseBooksOfSpells;
            _booksOfSpells.AbilityLevelUp += AbilityLevelUp;

            _waiteHotkeys += HotkeyMainAbilities;
            
            _booksOfSpells.InitBooks(_expirience);
        }


        public bool CheckingPressedKeys()
        {
            if (Input.GetKeyDown(_cancelAbilityKey))
            {
                CancelAbility();
                return true;
            }

            _waiteHotkeys?.Invoke();

            if (Input.GetKeyDown(_booksOfSpells.HotkeyOpenBook) && _booksOfSpells.IsOpenBook == false)
            {
                _booksOfSpells.OpenBook();
            }

            if (Input.GetKeyDown(_booksOfSpells.HotkeyCloseBook) && _booksOfSpells.IsOpenBook)
            {
                _booksOfSpells.CloseBook();
            }

            return false;
        }

        public EAbilityStatus ConfirmAbility(Actor actor, Vector3 goundPoint)
        {
            var confirmAbility = _applyingAbility.ConfirmAbility(goundPoint, actor);
            CancelAbility();
            
            return confirmAbility;
        }

        public void CancelAbility()
        {
            if (_applyingAbility != null)
            {
                _applyingAbility.CancelAbility();
                _applyingAbility = null;
            }
        }

        public Ability[] GetMainAbilities()
        {
            return _mainAbilities.ToArray();
        }

        public BooksOfSpells GetBooksOfSpells()
        {
            return _booksOfSpells;
        }

        public bool IsWaitApplyingAbility()
        {
            if (_applyingAbility != null)
            {
                return _applyingAbility.AbilityStatus == EAbilityStatus.NeedToConfirm;
            }

            return false;
        }

        private void HotkeyMainAbilities()
        {
            foreach (var ability in _mainAbilities)
            {
                if (Input.GetKeyDown(ability.HotKey))
                {
                    if(_applyingAbility != null) CancelAbility();
                    
                    _applyingAbility = ability;
                    _applyingAbility.TryApplyingAbility();

                }
            }
        }

        private void AbilityLevelUp(Ability ability)
        {
            if (_mainAbilities.Contains(ability))
            {
                var abilityUnit = _mainAbilities.First(f =>
                    f.GetAbilityDescription().AbilityName == ability.GetAbilityDescription().AbilityName);
                
                abilityUnit.AbilityLevelUp();
            }
            else
            {
                ability.TryActivateAbility += TryUseSpell;
                _mainAbilities.Add(ability);
            }
        }
        
        private void TryUseSpell(Ability ability)
        {
            if(_applyingAbility != null) _applyingAbility.CancelAbility();
            
            _applyingAbility = ability;
        }

        private void OpenBooksOfSpells()
        {
            _waiteHotkeys -= HotkeyMainAbilities;
            _waiteHotkeys += _booksOfSpells.WaiteHotkey;
        }

        private void CloseBooksOfSpells()
        {
            _waiteHotkeys -= _booksOfSpells.WaiteHotkey;
            _waiteHotkeys += HotkeyMainAbilities;
        }

    }
}
