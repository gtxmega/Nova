using System;
using UnityEngine;

namespace GameCore.Attributes.Expirience
{
    public class ExpirienceAttributes : MonoBehaviour, IExpirience
    {
        public event Action<float, float> OnChangeExpirience;
        public event Action<int> OnLevelUp;

        public int MaxLevel => _maxLevel;
        public int CurrentLevel => _currentLevel;
        public float CurrentExpirience => _currentExpirience;
        public float NeedExpirienceLevelUp => _needExpirience;
        public int AbilityPoints => _abilityPoints;

        [SerializeField] private int _maxLevel;
        [SerializeField] private int _currentLevel;
        [SerializeField] private float _currentExpirience;
        [SerializeField] private float _needExpirience;

        private int _abilityPoints = 1;
        
        
        public void AddExpirience(float countExpirience)
        {
            if(countExpirience <= 0) return;

            ChangeExpirience(countExpirience);
        }

        private void ChangeExpirience(float amount)
        {
            if (_currentLevel >= _maxLevel)
            {
                _currentExpirience = _needExpirience;
                return;
            }
            
            var expirience = _currentExpirience + amount;
            if (expirience >= _needExpirience)
            {
                expirience = expirience - _needExpirience;
                _currentLevel++;
                _abilityPoints++;
                
                AddExpirience(expirience);
                
                OnLevelUp?.Invoke(_currentLevel);
            }

            _currentExpirience = expirience;
            OnChangeExpirience?.Invoke(_currentExpirience, _needExpirience);

        }
        

        public void ChangeAbilityPoints(int amount)
        {
            _abilityPoints += amount;
        }
    }
}
