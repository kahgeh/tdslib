using System;
using System.IO;

namespace TdsLib.Utility
{
    public static class BinaryReaderExtensions
    {
        public static ushort ReadReverseUInt16(this BinaryReader reader)
        {
            return BitConverter.ToUInt16(reader.ReadBytes(2).Reverse());
        }

        public static UInt32 ReadReverseUInt32(this BinaryReader reader)
        {
            return BitConverter.ToUInt32(reader.ReadBytes(4).Reverse());
        }
    }
}