using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TdsLib.Errors;

namespace TdsLib.Packets
{
    public class TdsStream
    {
        public static TdsReadResult Read(byte[] bytes, IEnumerable<PacketType> allowedPacketTypes)
        {
            using (var stream = new MemoryStream(bytes))
            using (var reader = new BinaryReader(stream))
            {
                var raw = reader.ReadByte();
                var (packet, type) = LoadRequestPacket(reader, raw);

                return new TdsReadResult
                {
                    Error = allowedPacketTypes.Count(type => (byte)type == raw) < 1 ?
                                new InvalidTokenDetected(type, raw, allowedPacketTypes) : null,
                    Packet = packet,
                    Type = type,
                    RawType = raw,
                };
            }
        }

        private static (RequestPacket, PacketType?) LoadRequestPacket(BinaryReader reader, byte raw)
        {
            RequestPacket packet = null;
            PacketType? type = null;
            if (Enum.IsDefined(typeof(PacketType), Convert.ToInt32(raw)))
            {
                type = (PacketType)raw;
                packet = CreatePacket(type.Value);
                packet.Load(reader, 0);
            }

            return (packet, type);
        }

        private static RequestPacket CreatePacket(PacketType type)
        {
            switch (type)
            {
                case PacketType.PreLogin:
                    return new PreLoginRequest();
                default:
                    throw new System.Exception("Type unexpected");
            }
        }
    }
}