namespace TdsLib.Packets.Options
{
    public enum EncryptionOption
    {
        ENCRYPT_OFF = 0x00,
        ENCRYPT_ON = 0x01,
        ENCRYPT_NOT_SUP = 0x02,
        ENCRYPT_REQ = 0x03,
        ENCRYPT_CLIENT_CERT = 0x80
    }
}
