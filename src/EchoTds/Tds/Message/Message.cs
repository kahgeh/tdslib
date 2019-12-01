using System;
using System.IO;

namespace TdsLib.Message
{
    public abstract class Message
    {
        protected string Indent { get { return "  "; } }
        public byte Type { get; set; }
        public byte Status { get; set; }
        public UInt16 Length { get; set; }
        public UInt16 Channel { get; set; }
        public byte PacketNumber { get; set; }
        public byte Window { get; set; }

        protected Message(MessageType type)
        {
            Type = (byte)type;
        }

        protected BinaryReader ReadHeader(BinaryReader reader)
        {
            Status = reader.ReadByte();
            Length = reader.ReadReverseUInt16();
            Channel = reader.ReadReverseUInt16();
            PacketNumber = reader.ReadByte();
            Window = reader.ReadByte();
            return reader;
        }

        public override string ToString()
        {
            return $"Packet - {Enum.GetName(typeof(MessageType), Type)}\n" +
                $"{Indent}Type: {Type} \n" +
                $"{Indent}Status: {Status}\n" +
                $"{Indent}Length: {Length}\n" +
                $"{Indent}Channel: {Channel}\n" +
                $"{Indent}PacketNumber: {PacketNumber}\n" +
                $"{Indent}Window: {Window}";
        }

        public abstract void Load(BinaryReader reader, int callIndex);
    }
}