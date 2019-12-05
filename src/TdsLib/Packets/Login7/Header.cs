using System;
using System.IO;
using TdsLib.Utility;

namespace TdsLib.Packets.Login7
{
    public class Header
    {
        private string Indent { get { return "   "; } }
        public UInt32 Length { get; set; }
        public UInt32 TDSVersion { get; set; }
        public UInt32 PacketSize { get; set; }
        public UInt32 ClientProgVer { get; set; }
        public UInt32 ClientPID { get; set; }
        public UInt32 ConnectionID { get; set; }

        public OptionFlags1 OptionFlags1 { get; set; }
        public byte OptionFlags2 { get; set; }
        public byte TypeFlags { get; set; }
        public byte ReservedFlags { get; set; }
        public UInt32 TimeZone { get; set; }
        public UInt32 Collation { get; set; }

        public Header(UInt32 length)
        {
            Length = length;
        }

        public void Read(BinaryReader reader)
        {
            Length = reader.ReadUInt32();
            TDSVersion = reader.ReadUInt32();
            PacketSize = reader.ReadUInt32();
            ClientProgVer = reader.ReadReverseUInt32();
            ClientPID = reader.ReadUInt32();
            ConnectionID = reader.ReadUInt32();
            OptionFlags1 = new OptionFlags1();
            OptionFlags1.Read(reader);
            OptionFlags2 = reader.ReadByte();
            TypeFlags = reader.ReadByte();
            ReservedFlags = reader.ReadByte();
            TimeZone = reader.ReadUInt32();
            Collation = reader.ReadUInt32();
        }

        public override string ToString()
        {
            return $"{Indent}{nameof(Length)}: {Length}\n" +
                $"{Indent}{nameof(TDSVersion)}: 0x{TDSVersion.ToString("x")}\n" +
                $"{Indent}{nameof(PacketSize)}: {PacketSize}\n" +
                $"{Indent}{nameof(ClientProgVer)}: {ClientProgVer}\n" +
                $"{Indent}{nameof(ClientPID)}: {ClientPID}\n" +
                $"{Indent}{nameof(ConnectionID)}: {ConnectionID}\n" +
                OptionFlags1.ToString(true) +
                OptionFlags1.ToString() +
                $"{nameof(OptionFlags2)}: 0x{OptionFlags2.ToString("x").PadLeft(2, '0').Substring(0, 2)}\n" +
                $"{nameof(TypeFlags)}: 0x{TypeFlags.ToString("x").PadLeft(2, '0').Substring(0, 2)}\n" +
                $"{nameof(ReservedFlags)}: 0x{ReservedFlags.ToString("x").PadLeft(2, '0').Substring(0, 2)}\n" +
                $"{nameof(TimeZone)}: 0x{TimeZone.ToString("x").PadLeft(8, '0').Substring(0, 8)}\n" +
                $"{nameof(Collation)}: 0x{Collation.ToString("x").PadLeft(8, '0').Substring(0, 8)}\n";
        }

    }
}