
using UnityEngine;

namespace GameCore.Abilities
{
    [CreateAssetMenu(fileName = "Ability description", menuName = "Abilities/Ability description")]
    public class AbilityDescription : ScriptableObject
    {
        public Sprite AbilitySprite => _abilitySprite;
        public string AbilityName => _abilityName;
        public string Description => _abilityDescription;
        
        [SerializeField] private Sprite _abilitySprite;
        [SerializeField] private string _abilityName;
        [SerializeField] private string _abilityDescription;

    }
}
