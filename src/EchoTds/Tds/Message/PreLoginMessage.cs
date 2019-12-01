using System.IO;

namespace TdsLib.Message
{
    public abstract class PreLoginMessage
    {
        public abstract BinaryReader Read(BinaryReader reader);
    }
}