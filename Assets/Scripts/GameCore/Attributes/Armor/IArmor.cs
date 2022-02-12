using System;

namespace GameCore.Attributes.Armor
{
    public interface IArmor
    {
        event Action<IArmor> OnChangeArmor;

        float Armor { get; }
        float BaseArmor { get; }
        float BonusArmor { get; }
        float DecreaseArmor { get; }

        float HybridResistance { get; }
        float PhysicsResistance { get; }
        float MagicResistance { get; }

        float DecreaseHybridResistance { get; }
        float DecreaseMagicResistance { get; }

        void RecalculateArmor(int unitLevel);
        float CalculateDamageByResistance(float damage, EDamageType damageType);
    }
}
