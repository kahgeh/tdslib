using System;
using System.IO;

namespace TdsLib.Packets.Options
{
    public class Encryption : Option
    {
        public EncryptionOption Option { get; set; }

        public Encryption()
        {
            Token = (byte)OptionToken.ENCRYPTION;
            Length = 1;
        }

        public override byte[] GetData()
        {
            return new[]{
                (byte)Option
            };
        }

        public override void ReadData(BinaryReader reader)
        {
            Option = (EncryptionOption)reader.ReadByte();
        }

        public override string ToString()
        {
            return $"{base.ToString()}" +
                $"{Indent}Option: {Enum.GetName(typeof(EncryptionOption), Option)}\n";
        }
    }
}