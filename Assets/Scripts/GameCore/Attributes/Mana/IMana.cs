using System;

namespace GameCore.Attributes.Mana
{
    public interface IMana
    {
        event Action<float, float> OnChangeMana;
        
        float MaxMana { get; }
        float CurrentMana { get; }
        float RegenerationMana { get; }

        bool TryChangeCurrentMana(float amount);
        void ChangeMaxMana(float amount);
        void RegenerationTick();
    }
}
