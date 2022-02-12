using GameCore.Actors;
using UnityEngine;

using GameCore.StateMachines;

namespace GameCore
{
    public class AttackStateMachine : MonoBehaviour
    {
        [Header("Attack states, StateMachine")]
        [SerializeField] private State _idleState;
        [SerializeField] private State _attackState;
        [SerializeField] private State _reloadState;
        [SerializeField] private State _findTargetState;

        private StateMachine _stateMachine;
        private Actor _ownerActor;

        private void Awake()
        {
            _ownerActor = GetComponent<Actor>();

            InitStateMachine();
        }

        private void Update()
        {
            _stateMachine.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            _stateMachine.CurrentState.PhysicsUpdate();
        }

        private void InitStateMachine()
        {
            _stateMachine = new StateMachine();

            _idleState.InitializeState(_ownerActor, _stateMachine);
            _attackState.InitializeState(_ownerActor, _stateMachine);
            _reloadState.InitializeState(_ownerActor, _stateMachine);
            _findTargetState.InitializeState(_ownerActor, _stateMachine);

            _stateMachine.InitializeMachine(_findTargetState);
        }

    }
}
