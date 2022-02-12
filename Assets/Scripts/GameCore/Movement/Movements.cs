using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace GameCore.Movement
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Movements : MonoBehaviour, IMovement, IMovementValues, IChangeMovementValues
    {
        public event Action<bool> OnDestination;

        public float MaxSpeed => _maxSpeed;
        public float BaseSpeed => _baseSpeed;

        public float BonusSpeed => _bonusSpeed;
        public float DecreasingSpeed => _decreasingSpeed;
        

        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _baseSpeed;
        [SerializeField] private float _minDistance;

        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Transform _transform;

        private float _bonusSpeed;
        private float _decreasingSpeed;
        
        private Vector3 _destinationPoint;

        private Coroutine _coroutineWaitingDestination;

        private void Awake()
        {
            if (_agent == null) _agent = GetComponent<NavMeshAgent>();
            if (_transform == null) _transform = GetComponent<Transform>();

            _agent.isStopped = true;

            RecalculateSpeedForAgent();
        }

        private IEnumerator WaitingDestination()
        {
            while(_agent.isStopped == false)
            {
                var sqrDistance = (_transform.position - _destinationPoint).sqrMagnitude;
                if(sqrDistance <= _minDistance)
                {
                    StopMovement();
                    OnDestination?.Invoke(true);
                    
                    yield break;
                }

                yield return null;
            }
        }
        

        public void SetDestination( Vector3 point )
        {
            _destinationPoint = point;
            _agent.SetDestination(point);
            _agent.isStopped = false;

            if(_coroutineWaitingDestination != null)
                _coroutineWaitingDestination = StartCoroutine(WaitingDestination());
        }
        

        public void StopMovement()
        {
            _agent.isStopped = true;
            _agent.ResetPath();
        }

        public void ChangeBaseMoveSpeed(float amount)
        {
            _baseSpeed += amount;
            RecalculateSpeedForAgent();
        }

        public void ChangeBonusMoveSpeed(float amount)
        {
            _bonusSpeed += amount;
            RecalculateSpeedForAgent();
        }

        public void EncreasingMoveSpeed(float amount)
        {
            if (amount <= 0.0f) throw new SystemException(name + ": EncreasingMoveSpeed - needed amount > 0:" + amount);
            
            ChangeDecreasingMoveSpeed(amount);
        }

        public void DecreasingMoveSpeed(float amount)
        {
            if (amount >= 0.0f) throw new SystemException(name + ": DecreasingMoveSpeed - needed amount < 0:" + amount);

            ChangeDecreasingMoveSpeed(amount);
        }

        private void ChangeDecreasingMoveSpeed(float amount)
        {
            _decreasingSpeed += amount;
            RecalculateSpeedForAgent();
        }

        private void RecalculateSpeedForAgent()
        {
            var speed = (_baseSpeed + _bonusSpeed) - _decreasingSpeed;
            _agent.speed = Mathf.Clamp(speed, 1.0f, _maxSpeed);
        }


    }
}
