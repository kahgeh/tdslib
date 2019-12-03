using TdsLib.Packets;
using TdsLib.StateMachine.Scaffold;

namespace TdsLib.StateMachine
{
    public class Login : State
    {
        public Login()
        {
            AllowPacketTypes = new[] { PacketType.Tds7Login };
        }

        public override IResult Execute(TdsReadResult tdsReadResult, Session session)
        {
            throw new System.NotImplementedException();
        }
    }
}