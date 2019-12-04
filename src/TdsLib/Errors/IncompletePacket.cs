using TdsLib.Packets;
using TdsLib.Utility;

namespace TdsLib.Errors
{
    public class IncompletePacket : TdsLibError
    {
        public byte[] IncompleteBytes { get; }
        public RequestPacket RequestPacket { get; }
        public IncompletePacket(byte[] incompleteBytes, RequestPacket requestPacket)
            : base($"{requestPacket.GetLoadingProgress().GetDisplayText()}, remaining {incompleteBytes.Length} bytes is incomplete")
        {

        }
    }
}