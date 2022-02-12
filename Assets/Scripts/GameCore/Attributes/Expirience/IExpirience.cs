using System;
using UnityEngine;

namespace GameCore.Attributes.Expirience
{
    public interface IExpirience
    {
        event Action<float, float> OnChangeExpirience;
        event Action<int> OnLevelUp;

        int MaxLevel { get; }
        int CurrentLevel { get; }
        
        float CurrentExpirience { get; }
        float NeedExpirienceLevelUp { get; }
        int AbilityPoints { get; }
        void ChangeAbilityPoints(int amount);

        void AddExpirience(float countExpirience);
    }
}
