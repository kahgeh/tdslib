namespace TdsLib.Packets.Login7.Response
{
    public class EnvChange : Record
    {
        public EnvChangeType ChangeType { get; }
        public byte Size { get; set; }
        public string NewValue { get; set; }
        public string OldValue { get; set; }
        public EnvChange(EnvChangeType changeType) : base(TokenType.EnvChange)
        {
            ChangeType = changeType;
        }
    }
}