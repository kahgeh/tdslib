using System;
using System.IO;
using TdsLib.Errors;
using TdsLib.Packets.Login7;
using TdsLib.Utility;

namespace TdsLib.Packets
{
    public class LoginRequest : RequestPacket
    {
        Login7.Header Header { get; set; }
        public LoginRequest()
            : base(PacketType.Tds7Login)
        {

        }

        protected override IncompletePacket ReadBody(BinaryReader reader)
        {
            Header = new Header();
            Header.Read(reader);
            Console.WriteLine($"****{ToString()}");
            return null;
        }

        public override string ToString()
        {
            return $"{base.ToString()}\n" +
                   Header.ToString();
        }
    }
}