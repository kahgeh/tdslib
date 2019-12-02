namespace TdsLib.Errors
{
    public class PacketError
    {
        public string Message { get; }

        public PacketError(string message)
        {
            Message = message;
        }
    }
}