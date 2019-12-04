using System.Collections;

namespace TdsLib.Utility
{
    public static class BitArrayExtensions
    {
        public static byte ToByte(this BitArray bits)
        {
            byte[] bytes = new byte[1];
            bits.CopyTo(bytes, 0);
            return bytes[0];
        }
    }
}
