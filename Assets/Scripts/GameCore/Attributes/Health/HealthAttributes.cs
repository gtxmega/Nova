using System;
using UnityEngine;

namespace GameCore.Attributes.Health
{
    public class HealthAttributes : MonoBehaviour, IHealth
    {
        public event Action<float, float> OnChangeHealth;
        public bool IsDead { get { return _currentHealth <= 0.0f; } }
        public float MaxHealth => _maxHealth;
        public float CurrentHealth => _currentHealth;
        public float RegenerationHealth => _regenerationHealth;
        

        [SerializeField] private float _maxHealth;
        [SerializeField] private float _currentHealth;
        [SerializeField] private float _regenerationHealth;

        public bool TryChangeCurrentHealth(float amount)
        {
            var tempHealth = _currentHealth + amount;
            if (tempHealth < 0.0f) return false;
            
            _currentHealth = tempHealth;
            _currentHealth = Mathf.Clamp(_currentHealth, 0.0f, _maxHealth);

            OnChangeHealth?.Invoke(_currentHealth, _maxHealth);

            return true;
        }

        public void ChangeMaxHealth(float amount)
        {
            var percentMana = _currentHealth / _maxHealth;
            
            _maxHealth += amount;
            _currentHealth = _maxHealth * percentMana;
            
            OnChangeHealth?.Invoke(_currentHealth, _maxHealth);
        }

        public void RegenerationTick()
        {
            if(_currentHealth < _maxHealth)
            {
                TryChangeCurrentHealth(_regenerationHealth * Time.deltaTime);
            }
        }

    }
}
