using System;
using System.Text;

namespace TdsLib.Utility
{
    public static class BytesExtensions
    {
        public static byte[] Reverse(this byte[] bytes)
        {
            var newBytes = bytes.Clone() as byte[];
            Array.Reverse(newBytes);
            return newBytes;
        }

        public static void HexDump(this byte[] bytes)
        {
            var byteCnt = 0;
            foreach (var b in bytes)
            {
                var byteString = $"{b.ToString("X")}".PadLeft(2, '0');
                Console.Write($"{byteString} ");
                byteCnt++;
                if (byteCnt % 8 == 0)
                {
                    Console.Write(" ");
                }
                if (byteCnt % 16 == 0)
                {
                    Console.Write("\n");
                }
            }
        }

        public static string GetHexText(this byte[] bytes)
        {
            var builder = new StringBuilder();
            foreach (var b in bytes)
            {
                var byteString = $"{b.ToString("x")}".PadLeft(2, '0');
                builder.Append(byteString);
            }
            return builder.ToString();
        }
    }


}