using System;
using System.IO;

namespace TdsLib.Packets
{
    public class PreLoginRequest : RequestPacket
    {
        public PreLoginMessage Message { get; set; }
        public PreLoginRequest() :
            base(PacketType.PreLogin)
        {

        }

        public override void Load(BinaryReader reader)
        {
            ReadHeader(reader);
            Message = new OptionsMessage();
            Message.Read(reader);
            State = PacketState.Valid;

        }

        public override string ToString()
        {
            return $"{base.ToString()}\n" +
                   Message.ToString();
        }
    }
}