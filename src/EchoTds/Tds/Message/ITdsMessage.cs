using System;

namespace TdsLib.Message
{
    public interface ITdsMessage
    {
        byte[] ToBytes();
    }
}