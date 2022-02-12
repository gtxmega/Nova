using GameCore.Actors;
using UnityEngine;

using GameCore.StateMachines;

namespace GameCore.Damageable.StateMachine.States
{
    public class PrepareAttackState : State
    {
        [SerializeField] private float _timeBeforeAttack;
        [SerializeField] private float _timeAfterAttack;

        [Header("Transitional states")]
        [SerializeField] private State _attackState;


        private float _timer;
        private float _prepareAttackTime;

        private EPrepareAttack _timerState;

        private Actor _enemyTarget;

        public override void EnterState(params object[] args)
        {
            base.EnterState(args);

            if (args != null && args.Length > 0)
            {
                _enemyTarget = args[0] as Actor;
                _timerState = (EPrepareAttack)args[1];
            }

            SetPrepareTimer();

        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            _timer += Time.deltaTime;
            WaiteEndTimerAndSwitchState();
        }

        private void WaiteEndTimerAndSwitchState()
        {
            if(_timer >= _prepareAttackTime)
            {
                _timer = 0.0f;
                _stateMachine.SwitchingStates(_attackState, _enemyTarget);
            }
        }

        private void SetPrepareTimer()
        {
            switch (_timerState)
            {
                case EPrepareAttack.Time_Before:
                    _prepareAttackTime = _timeBeforeAttack;
                    break;
                case EPrepareAttack.Time_After:
                    _prepareAttackTime = _timeAfterAttack;
                    break;
                default:
                    break;
            }
        }
    }
}
