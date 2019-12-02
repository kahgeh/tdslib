using TdsLib.Packets;
using TdsLib.StateMachine.Scaffold;

namespace TdsLib.StateMachine
{
    public class End : State
    {
        public override IResult Execute(TdsReadResult tdsReadResult, Session session)
        {
            throw new System.NotImplementedException();
        }
    }
}