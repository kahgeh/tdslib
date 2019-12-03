namespace TdsLib.Errors
{
    public class TerminationSignalReceived : TdsLibError
    {
        public TerminationSignalReceived()
            : base("Termination signal received")
        {
        }
    }
}