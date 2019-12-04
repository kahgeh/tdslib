using System.Collections.Generic;
using TdsLib.Packets;

namespace TdsLib.Errors
{
    public class InvalidTokenDetected : TdsLibError
    {
        public IEnumerable<PacketType> AllowedPacketTypes { get; }
        public byte Raw { get; }
        public PacketType? Type { get; }
        public InvalidTokenDetected(PacketType? type, byte raw, IEnumerable<PacketType> allowedPacketTypes) :
        base($"{allowedPacketTypes.GetCommaDelimitedText()} expected but \"{raw}\"({type.GetName()}) detected.")
        {
            Type = type;
            Raw = raw;
            AllowedPacketTypes = allowedPacketTypes;
        }
    }
}