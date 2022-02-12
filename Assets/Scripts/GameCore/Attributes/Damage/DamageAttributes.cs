using System;
using GameCore.CustomDataStruct;
using UnityEngine;

namespace GameCore.Attributes.Damage
{
    public class DamageAttributes : MonoBehaviour, IDamage
    {
        public event Action<FloatRange, float, float> OnChangeDamage;

        public FloatRange Damage => _damage;
        public float BonusDamage => _bonusDamage;
        public float DecreaseDamage => _decreaseDamage;
        public float AttackRange => _attackRange;
        public EDamageType DamageType => _damageType;


        [SerializeField] private FloatRange _damage;
        [SerializeField] private float _bonusDamage;
        [SerializeField] private float _decreaseDamage;
        [SerializeField] private float _attackRange;
        [SerializeField] private EDamageType _damageType;
    }
}
