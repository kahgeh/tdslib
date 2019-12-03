using System.Linq;
using TdsLib.Errors;
using TdsLib.Packets;
using TdsLib.Packets.Options;
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
            if (tdsReadResult.Error != null)
            {
                return tdsReadResult.Error;
            }

            var preLoginRequest = tdsReadResult.Packet as PreLoginRequest;
            var optionsMessage = preLoginRequest.Message as OptionsMessage;
            var threadId = optionsMessage.Options.FirstOrDefault(option => option.Token == (byte)OptionToken.THREADID) as ThreadId;
            if (threadId != null && threadId.Id.HasValue)
            {
                session.ClientThreadId = threadId.Id.Value;
            }
            return new PreLoginResponse();
        }
    }

}