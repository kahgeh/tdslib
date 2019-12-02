using System.Linq;
using System.Threading.Tasks;
using TdsLib.Errors;
using TdsLib.Packets;
using TdsLib.StateMachine.Scaffold;

namespace TdsLib.StateMachine
{
    public class PreLogin : State
    {
        public PreLogin()
        {
            AllowPacketTypes = new[] { PacketType.PreLogin };
        }

        public override IResult Execute(TdsReadResult tdsReadResult, Session session)
        {
            if (tdsReadResult.Error != null &&
                tdsReadResult.Error is InvalidTokenDetected)
            {
                return (InvalidTokenDetected)tdsReadResult.Error;
            }

            var preLoginRequestPacket = tdsReadResult.Packet;
            return new PreLoginResponse();
        }
    }

}