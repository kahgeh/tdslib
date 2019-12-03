using System.IO;

namespace TdsLib.Packets
{
    public abstract class PreLoginMessage
    {
        public abstract BinaryReader Read(BinaryReader reader);
    }
}