using System;
using System.Collections.Generic;
using System.IO;
using TdsLib.Utility;

namespace TdsLib.Packets
{
    public abstract class Packet
    {
        protected string Indent { get { return "  "; } }
        public byte Type { get; set; }
        public byte Status { get; set; }
        public UInt16 Length { get; set; }
        public UInt16 Channel { get; set; }
        public byte PacketNumber { get; set; }
        public byte Window { get; set; }

        protected Packet(PacketType type)
        {
            Type = (byte)type;
        }

        public override string ToString()
        {
            return $"Packet - {Enum.GetName(typeof(PacketType), Type)}\n" +
                $"{Indent}Type: {Type} \n" +
                $"{Indent}Status: {Status}\n" +
                $"{Indent}Length: {Length}\n" +
                $"{Indent}Channel: {Channel}\n" +
                $"{Indent}PacketNumber: {PacketNumber}\n" +
                $"{Indent}Window: {Window}";
        }

        public virtual byte[] ToBytes()
        {
            var bytes = new List<byte>();
            bytes.Add(Type);
            bytes.Add(Status);
            bytes.AddRange(BitConverter.GetBytes(Length).Reverse());
            bytes.AddRange(BitConverter.GetBytes(Channel).Reverse());
            bytes.Add(PacketNumber);
            bytes.Add(Window);
            return bytes.ToArray();
        }
    }

    public abstract class RequestPacket : Packet
    {
        public PacketState State { get; set; }
        public abstract void Load(BinaryReader reader, int callIndex);

        protected RequestPacket(PacketType type) : base(type)
        {
            State = PacketState.Empty;
        }

        protected BinaryReader ReadHeader(BinaryReader reader)
        {
            Status = reader.ReadByte();
            State = PacketState.Partial;

            Length = reader.ReadReverseUInt16();
            Channel = reader.ReadReverseUInt16();
            PacketNumber = reader.ReadByte();
            Window = reader.ReadByte();
            return reader;
        }
    }
}