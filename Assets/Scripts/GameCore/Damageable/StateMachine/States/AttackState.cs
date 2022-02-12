using GameCore.Actors;
using GameCore.Attributes.Damage;
using UnityEngine;

using Zenject;

namespace GameCore.Damageable.StateMachine.States
{
    using GameCore.StateMachines;

    public class AttackState : State
    {
        [Header("Bullet")]
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private GameObject _bulletPrefab;

        [Header("Transitional states")]
        [SerializeField] private State _reloadsState;
        [SerializeField] private State _findTargetState;


        [Inject] private IDamage _damageAttributes;

        [Inject] private IPoolObjects _bulletsPool;
        private readonly int _bulletsPoolLength = 20;
        
        private Actor _enemyTarget;
        private IDamageable _enemyDamagable;


        private void Awake()
        {
            _bulletsPool.CreatePoolObjects(_bulletPrefab, _bulletsPoolLength);
        }

        public override void EnterState(params object[] args)
        {
            base.EnterState();

            if(args.Length > 0)
            {
                _enemyTarget = args[0] as Actor;
                _enemyDamagable = _enemyTarget.GetComponent<IDamageable>();
            }

        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if(InRangeAttack() && _enemyDamagable.IsDead() == false)
            {
                var bullet = _bulletsPool.GetAvailableObjectFromPool<Bullets>();
                bullet.InitializeBullet(
                    _damageAttributes.Damage.GetRandom(),
                    _damageAttributes.DamageType,
                    _bulletSpeed,
                    _enemyDamagable);

                _stateMachine.SwitchingStates(_reloadsState, _enemyTarget, EPrepareAttack.Time_After);
            }
            else
            {
                _stateMachine.SwitchingStates(_findTargetState);

            }
        }

        private bool InRangeAttack()
        {
            var directionOwnerToTarget = _enemyTarget.ActorPosition - _ownerActor.ActorPosition;
            return directionOwnerToTarget.sqrMagnitude <= (_damageAttributes.AttackRange * _damageAttributes.AttackRange);
        }

    }
}
