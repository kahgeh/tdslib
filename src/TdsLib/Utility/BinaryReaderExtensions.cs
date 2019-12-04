using System;
using System.IO;
using TdsLib.Errors;
using TdsLib.Packets;

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

        public static IncompletePacket ReadSection(this BinaryReader reader,
            RequestPacket requestPacket,
            byte originalLoadingProgress,
            Action<BinaryReader> read
            )
        {
            var originalStreamPosition = reader.BaseStream.Position;
            try
            {
                read(reader);
            }
            catch (EndOfStreamException)
            {
                var currentStreamPosition = reader.BaseStream.Position;
                reader.BaseStream.Position = originalStreamPosition;
                var incompleteBytes = reader.ReadBytes(int.MaxValue);
                return new IncompletePacket(incompleteBytes, requestPacket);
            }
            return null;
        }
    }
}