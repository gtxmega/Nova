using System;
using GameCore.Abilities;
using GameCore.Actors;
using GameCore.CustomDataStruct;
using GameCore.UI.Panels.Abilities;
using UnityEngine;

namespace GameCore.UI
{
    public class UIActorAbilitiesDisplaying : MonoBehaviour, UIDisplayingInfo
    {
        private AbilitiesPanel _abilitiesPanel;

        private IAbilityDisplying _abilityDisplying;

        private void Awake()
        {
            _abilitiesPanel = FindObjectOfType<AbilitiesPanel>();
        }


        public void Displaying(Actor actor)
        {
            if (actor.TryGetComponent<IAbilityDisplying>(out var _abilityDisplying))
            {
                _abilitiesPanel.ShowMainAbilitiesSlots(_abilityDisplying);
                
                if(actor.Team == EPlayerTeams.TeamHumans)
                    _abilitiesPanel.ShowBooksOfSpells(_abilityDisplying.GetBooksOfSpells());
            }
        }

        public void HideDisplaying()
        {
            _abilitiesPanel.HideAllAbilitiesSlots();
            _abilitiesPanel.HideBooksOfSpells();
        }
    }
}
