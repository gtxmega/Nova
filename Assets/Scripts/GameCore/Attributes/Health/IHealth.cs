
using System;

namespace GameCore.Attributes.Health
{
    public interface IHealth
    {
        event Action<float, float> OnChangeHealth;

        bool IsDead { get; }
        float MaxHealth { get; }
        float CurrentHealth { get; }
        float RegenerationHealth { get; }

        bool TryChangeCurrentHealth(float amount);
        void ChangeMaxHealth(float amount);
        void RegenerationTick();
    }
}
