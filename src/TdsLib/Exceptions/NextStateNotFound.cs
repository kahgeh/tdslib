using System;
using TdsLib.StateMachine.Scaffold;

namespace TdsLib.Exceptions
{
    public class NextStateNotFound : TdsLibException
    {
        public State State { get; }
        public string ResultName { get; }
        public NextStateNotFound(State state, string resultName) :
            base($"No next state exists when the input is {resultName} on {state.GetType().Name}")
        {
            State = state;
            ResultName = resultName;
        }
    }
}