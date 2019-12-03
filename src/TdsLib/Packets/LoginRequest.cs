using System.IO;

namespace TdsLib.Packets
{
    public class LoginRequest : RequestPacket
    {
        public LoginRequest(PacketType type)
            : base(PacketType.Tds7Login)
        {

        }

        public override void Load(BinaryReader reader)
        {
            throw new System.NotImplementedException();
        }
    }
}