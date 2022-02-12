using GameCore.Actors;
using UnityEngine;

namespace GameCore.StateMachines
{
    public abstract class State : MonoBehaviour
    {
        protected Actor _ownerActor;
        protected StateMachine _stateMachine;

        public virtual void InitializeState(Actor ownerActor, StateMachine stateMachine)
        {
            _ownerActor = ownerActor;
            _stateMachine = stateMachine;
        }

        public virtual void EnterState(params object[] args)
        {

        }

        public virtual void LogicUpdate()
        {

        }

        public virtual void PhysicsUpdate()
        {

        }

        public virtual void ExitState()
        {

        }



    }
}
