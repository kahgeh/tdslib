namespace TdsLib.Packets
{
    public enum OptionToken
    {
        VERSION = 0x00,
        ENCRYPTION = 0x01,
        INSTOPT = 0x02,
        THREADID = 0x03,
        MARS = 0x04,
        TRACEID = 0x05,
        FEDAUTHREQUIRED = 0x06,
        NONCEOPT = 0x07,
        TERMINATOR = 0xFF
    }
}