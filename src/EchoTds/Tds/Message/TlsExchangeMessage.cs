using System.IO;

namespace TdsLib.Message
{
    public class TlsExchangeMessage : PreLoginMessage
    {
        public override BinaryReader Read(BinaryReader reader)
        {
            return reader;
        }
    }
}