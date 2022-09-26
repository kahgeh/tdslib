using System;

namespace TdsLib.Packets.Login7.Response
{
    public abstract class Record
    {
        public TokenType Type { get; }
        public UInt16 Length { get; set; }
        protected Record(TokenType type)
        {
            Type = type;
        }

        public virtual byte[] ToBytes()
        {
            var lengthBytes = BitConverter.GetBytes(Length);
            return new byte[]{
                (byte)Type,
                lengthBytes[0],
                lengthBytes[1]
            };
        }

        public UInt16 GetSize()
        {
            var bytes = ToBytes();
            return (UInt16)bytes.Length;
        }

    }
}