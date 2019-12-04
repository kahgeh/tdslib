using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TdsLib.Errors;
using TdsLib.Exceptions;

namespace TdsLib.Packets
{
    public class TdsStream
    {
        public static TdsReadResult Read(byte[] bytes, IEnumerable<PacketType> allowedPacketTypes)
        {
            // todo : what if the bytes is incomplete
            // - return an Incomplete error ( retaining related bytes of partially read section)
            // - each RequestPacket should have loading states 
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
                var incompletePacket = packet.Load(reader);
                if (incompletePacket != null)
                {
                    throw new TdsLibException(incompletePacket);
                }
            }

            return (packet, type);
        }

        private static RequestPacket CreatePacket(PacketType type)
        {
            switch (type)
            {
                case PacketType.PreLogin:
                    return new PreLoginRequest();
                case PacketType.Tds7Login:
                    return new LoginRequest();
                default:
                    throw new System.Exception("Type unexpected");
            }
        }
    }
}