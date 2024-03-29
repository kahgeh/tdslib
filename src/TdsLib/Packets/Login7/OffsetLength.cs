using System;
using System.IO;
using TdsLib.Utility;

namespace TdsLib.Packets.Login7
{
    public class OffsetLength
    {
        public UInt16 Offset { get; set; }
        public UInt16 Length { get; set; }

        public void Read(BinaryReader reader)
        {
            Offset = reader.ReadUInt16();
            Length = reader.ReadUInt16();
        }
    }
}