using System;
using UnityEngine;

namespace GameCore.Attributes.Armor
{
    [Serializable]
    public class ArmorAttributes : MonoBehaviour, IArmor
    {
        public event Action<IArmor> OnChangeArmor;

        public float Armor => _armor;
        public float BaseArmor => _baseArmor;
        public float BonusArmor => _bonusArmor;
        public float DecreaseArmor => _decreaseArmor;

        public float HybridResistance => _hybridResistance;
        public float PhysicsResistance => _physicsResistance;
        public float MagicResistance => _magicResistance;

        public float DecreaseHybridResistance => _decreaseHybridResistance;
        public float DecreaseMagicResistance => _decreaseMagicResistance;


        [Header("Armor")]
        [SerializeField] private float _armor;
        [SerializeField] private float _baseArmor;

        [Header("Resistance")]
        [SerializeField] private float _hybridResistance;
        [SerializeField] private float _physicsResistance;
        [SerializeField] private float _magicResistance;

        [Header("Armor attributes per level")]
        [SerializeField] private float _armorPerLevel;
        [SerializeField] private float _resistancePerArmorUnit;

        private float _bonusArmor;

        private float _decreaseArmor;
        private float _decreaseHybridResistance;
        private float _decreaseMagicResistance;


        public float CalculateDamageByResistance(float damage, EDamageType damageType)
        {
            float resistance = 0.0f;

            switch (damageType)
            {
                case EDamageType.Hybrid:
                    resistance = _hybridResistance;
                    break;
                case EDamageType.Physics:
                    resistance = _physicsResistance;
                    break;
                case EDamageType.Magic:
                    resistance = _magicResistance;
                    break;
                default:
                    break;
            }

            return damage - (damage / 100.0f * resistance);
        }

        public void ChangeBonusArmor(float bonusArmor)
        {
            _bonusArmor += bonusArmor;
            RecalculatePhysicsResistance();

            OnChangeArmor(this);
        }

        public void RecalculateArmor(int unitLevel)
        {
            _armor = _armorPerLevel * unitLevel;
            RecalculatePhysicsResistance();
        }

        private void RecalculatePhysicsResistance()
        {
            _physicsResistance = ((_armor + _baseArmor + _bonusArmor) - _decreaseArmor) * _resistancePerArmorUnit;
        }


    }
}
