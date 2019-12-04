using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TdsLib.Errors;
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
        public Dictionary<string, byte> LoadingProgresses { get; }
        public byte LoadingProgress { get; set; }

        protected RequestPacket(PacketType type) : base(type)
        {
            LoadingProgresses = new Dictionary<string, byte> { { nameof(ReadHeader), 0x01 } };
            LoadingProgress = (byte)PacketState.Empty;
        }

        public IncompletePacket Load(BinaryReader reader)
        {
            IncompletePacket incompletePacket;

            if ((incompletePacket = reader.ReadSection(
                this,
                LoadingProgress,
                ReadHeader)) != null)
            {
                return incompletePacket;
            }

            if ((incompletePacket = ReadBody(reader)) != null)
            {
                return incompletePacket;
            }

            return null;
        }

        protected abstract IncompletePacket ReadBody(BinaryReader reader);

        private Action<BinaryReader> ReadHeader => (reader) =>
         {
             Status = reader.ReadByte();
             Length = reader.ReadReverseUInt16();
             Channel = reader.ReadReverseUInt16();
             PacketNumber = reader.ReadByte();
             Window = reader.ReadByte();
             LoadingProgress = LoadingProgresses[nameof(ReadHeader)];
         };

        public (string packetName, int totalSteps, string stepName, byte loadingProgress) GetLoadingProgress()
        {
            var totalStep = LoadingProgresses.Count();
            var stepName = LoadingProgresses
                            .Where(kvp => kvp.Value == LoadingProgress)
                            .First()
                            .Key;
            return (GetType().Name, totalStep, stepName, LoadingProgress);
        }


    }
}