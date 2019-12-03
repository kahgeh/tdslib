namespace TdsLib.Errors
{
    public class NothingReturned : TdsLibError
    {
        public NothingReturned() : base("Nothing was read")
        {
        }
    }
}