using TdsLib.StateMachine.Scaffold;

namespace TdsLib.Errors
{
    public class TdsLibError : IResult
    {
        public string Message { get; }

        public TdsLibError(string message)
        {
            Message = message;
        }
    }
}