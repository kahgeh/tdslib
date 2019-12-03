using System.Collections.Generic;
using TdsLib.Errors;

namespace TdsLib.Packets
{
    public class TdsReadResult
    {
        public TdsLibError Error { get; set; }
        public RequestPacket Packet { get; set; }
        public byte RawType { get; set; }
        public PacketType? Type { get; set; }
    }

    public enum PacketState
    {
        Empty,
        Partial,
        Valid,
        Invalid
    }
}