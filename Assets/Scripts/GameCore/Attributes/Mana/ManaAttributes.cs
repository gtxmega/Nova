using System;
using UnityEngine;

namespace GameCore.Attributes.Mana
{
    public class ManaAttributes : MonoBehaviour, IMana
    {
        public event Action<float, float> OnChangeMana;
        public float MaxMana => _maxMana;
        public float CurrentMana => _currentMana;
        public float RegenerationMana => _regenerationMana;
        
        
        [SerializeField] private float _maxMana;
        [SerializeField] private float _currentMana;
        [SerializeField] private float _regenerationMana;


        public bool TryChangeCurrentMana(float amount)
        {
            var tempMana = _currentMana + amount;
            if (tempMana < 0.0f)
                return false;
            
            
            _currentMana = tempMana;
            _currentMana = Mathf.Clamp(_currentMana, 0.0f, _maxMana);
            

            OnChangeMana?.Invoke(_currentMana, _maxMana);
            return true;
        }

        public void ChangeMaxMana(float amount)
        {
            var percentMana = _currentMana / _maxMana;
            
            _maxMana += amount;
            _currentMana = _maxMana * percentMana;
            
            OnChangeMana?.Invoke(_currentMana, _maxMana);
        }

        public void RegenerationTick()
        {
            if(_currentMana < _maxMana)
            {
                TryChangeCurrentMana(_regenerationMana * Time.deltaTime);
            }
        }
    }
}
