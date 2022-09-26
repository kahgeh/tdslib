using System.Linq;
using TdsLib.Packets;
using TdsLib.Packets.Login7;
using TdsLib.Packets.Login7.Response;
using TdsLib.StateMachine.Scaffold;
using static TdsLib.Packets.Login7.VariableNames;
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
            var loginRequest = (LoginRequest)tdsReadResult.Packet;
            MapLoginRequestToSession(session, loginRequest);
            var loginResponse = new LoginResponse
            {
                Records = new Record[]{
                    new EnvChange(EnvChangeType.PacketSize),
                    new LoginAck()

                }
            };

            return loginResponse;
        }

        private static void MapLoginRequestToSession(Session session, LoginRequest loginRequest)
        {
            session.PacketSize = loginRequest.Header.PacketSize;
            session.AppName = (loginRequest
                                            .Data
                                            .FirstOrDefault(d => d.Name == AppName) as TextVariable)
                                            .Value;
            session.ClientHostName = (loginRequest
                                .Data
                                .FirstOrDefault(d => d.Name == HostName) as TextVariable)
                                .Value;
            session.UserName = (loginRequest
                                            .Data
                                            .FirstOrDefault(d => d.Name == UserName) as TextVariable)
                                            .Value;
        }
    }
}