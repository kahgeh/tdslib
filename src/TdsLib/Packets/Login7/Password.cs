using System;
using System.Text;

namespace TdsLib.Packets.Login7
{
    public class Password : Data
    {
        public string Value { get; set; }
        public override void Read(byte[] data)
        {
            var dataSpan = new Span<byte>(data);
            var relatedBytes = dataSpan.Slice(OffsetLength.Offset, OffsetLength.Length * 2).ToArray();
            var decryptedBytes = new byte[OffsetLength.Length];
            var decryptedByteIndex = 0;
            for (var i = 0; i < relatedBytes.Length; i++)
            {
                if ((i + 1) % 2 == 0)
                {
                    continue;
                }
                var decryptedByte = (byte)(relatedBytes[i] ^ 0xA5);
                decryptedBytes[decryptedByteIndex] = (byte)(((decryptedByte & 0x0F) << 4) | ((decryptedByte & 0xF0) >> 4));
                decryptedByteIndex++;
            }
            Value = Encoding.ASCII.GetString(decryptedBytes);
        }

        public override string ToString()
        {
            return $"{Indent}{nameof(Password)}: {Value}\n";
        }
    }
}