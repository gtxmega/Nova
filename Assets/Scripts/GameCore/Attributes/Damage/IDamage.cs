using System;
using GameCore.CustomDataStruct;

namespace GameCore.Attributes.Damage
{
    public interface IDamage
    {
        event Action<FloatRange, float, float> OnChangeDamage;

        FloatRange Damage { get; }
        float BonusDamage { get; }
        float DecreaseDamage { get; }
        float AttackRange { get; }
        EDamageType DamageType { get; }
    }
}

