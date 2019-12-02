using System.Collections.Generic;
using TdsLib.Packets;
using TdsLib.StateMachine.Scaffold;

namespace TdsLib.Errors
{
    public class InvalidTokenDetected : PacketError, IResult
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