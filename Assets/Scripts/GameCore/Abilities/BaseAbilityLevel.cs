using UnityEngine;

namespace GameCore.Abilities
{
    [CreateAssetMenu(fileName = "Base ability level", menuName = "Abilities/Base ability level")]
    public class BaseAbilityLevel : ScriptableObject
    {
        public float Damage => _damage;
        public float UsageDistance => _usageDistance;
        public float UsageArea => _usageArea;
        public float Cooldown => _cooldown;
        public float ManaCost => _manaCost;
        

        [SerializeField] private float _damage;
        [SerializeField] private float _usageDistance;
        [SerializeField] private float _usageArea;
        [SerializeField] private float _cooldown;
        [SerializeField] private float _manaCost;
    }
}
