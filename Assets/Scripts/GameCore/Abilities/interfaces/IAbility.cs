using System;
using UnityEngine;

using GameCore.UI.Panels.Abilities;

namespace GameCore.Abilities
{
    public interface IAbility
    {
        event Action<float, float> AbilityCooldownTick;
        event Action OnAbilityLevelUp;

        int MaxLevel { get; }
        int Level { get; }
        EAbilityStatus AbilityStatus { get; }

        AbilityDescription GetAbilityDescription();
        EAbilitySlotType GetAbilitySlotType();

    }
}
