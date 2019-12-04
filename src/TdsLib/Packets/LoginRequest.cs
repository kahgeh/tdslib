using System.IO;
using TdsLib.Errors;

namespace TdsLib.Packets
{
    public class LoginRequest : RequestPacket
    {
        public LoginRequest()
            : base(PacketType.Tds7Login)
        {

        }

        protected override IncompletePacket ReadBody(BinaryReader reader)
        {
            throw new System.NotImplementedException();
        }
    }
}