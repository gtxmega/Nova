
namespace GameCore.StateMachines
{
    public class StateMachine
    {
        public State CurrentState { get; private set; }


        public void InitializeMachine(State startingState, params object[] args)
        {
            CurrentState = startingState;

            startingState.EnterState(args);
        }

        public void SwitchingStates(State nextState, params object[] args)
        {
            CurrentState.ExitState();

            CurrentState = nextState;
            CurrentState.EnterState(args);
        }
    }
}
