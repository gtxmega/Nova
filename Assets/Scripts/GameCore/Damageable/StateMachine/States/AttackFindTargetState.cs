using GameCore.Actors;
using UnityEngine;


namespace GameCore.Damageable.StateMachine.States
{
    using GameCore.StateMachines;

    public class AttackFindTargetState : State
    {
        [SerializeField] private Transform _scanPointTransform;
        [SerializeField] private LayerMask _scanLayerMask;
        [SerializeField] private float _radiusScanEmemy;

        [Header("Prepare attack state")]
        [SerializeField] private State _prepareAttackState;


        private readonly int _bufferScanActorsLength = 5;
        private Collider[] _bufferScanActors;


        public override void EnterState(params object[] args)
        {
            base.EnterState();

            if (_bufferScanActors == null)
                _bufferScanActors = new Collider[_bufferScanActorsLength];
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            var countActorsInRadius = Physics.OverlapSphereNonAlloc(
                _scanPointTransform.position, 
                _radiusScanEmemy, 
                _bufferScanActors, 
                _scanLayerMask
                );
            
            var nearestTarget = FindNearestActorInBuffer(countActorsInRadius);
            if(nearestTarget != null)
            {
                _stateMachine.SwitchingStates(_prepareAttackState, nearestTarget, EPrepareAttack.Time_Before);
            }
        }

        private Actor FindNearestActorInBuffer(int bufferLength)
        {
            if (bufferLength == 0) return null;

            if(bufferLength == 1)
            {
                return _bufferScanActors[0].GetComponent<Actor>();
            }else
            {
                Vector3 directionOwnerToTarget = _bufferScanActors[0].GetComponent<Actor>().ActorPosition - _ownerActor.ActorPosition;
                float minimumSqrMagnitude = directionOwnerToTarget.sqrMagnitude;
                int minimumDistanceActorIndex = 0;

                for (int i = 1; i < bufferLength; ++i)
                {
                    directionOwnerToTarget = _bufferScanActors[i].GetComponent<Actor>().ActorPosition - _ownerActor.ActorPosition;
                    float nextQueueActorSqrMagnitude = directionOwnerToTarget.sqrMagnitude;

                    if(minimumSqrMagnitude >= nextQueueActorSqrMagnitude)
                    {
                        minimumSqrMagnitude = nextQueueActorSqrMagnitude;
                        minimumDistanceActorIndex = i;
                    }
                }

                return _bufferScanActors[minimumDistanceActorIndex].GetComponent<Actor>();
            }


        }


        #region ONLY_EDITOR

#if UNITY_EDITOR

        [Header("ONLY EDITOR")]
        public Color ScanSphereColor;
        public bool IsWireSphere;
        public bool DisplayingSphere;

        private void OnDrawGizmos()
        {
            if(DisplayingSphere == false) return;
            
            if (_scanPointTransform == null) return;

            Gizmos.color = ScanSphereColor;

            if (IsWireSphere)
            {
                Gizmos.DrawWireSphere(_scanPointTransform.position, _radiusScanEmemy);
            }else
            {
                Gizmos.DrawSphere(_scanPointTransform.position, _radiusScanEmemy);
            }
        }

#endif

        #endregion
    }
}
