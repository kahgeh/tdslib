using System;
using System.IO;

namespace TdsLib.Message.Options
{
    public class Encryption : Option
    {
        public bool On { get; set; }

        public Encryption()
        {
            Token = (byte)OptionToken.ENCRYPTION;
            Length = 1;
        }

        public override byte[] GetData()
        {
            return new[]{
                (byte)(On ? 1 : 0)
            };
        }

        public override void ReadData(BinaryReader reader)
        {
            On = reader.ReadByte() == 1;
        }

        public override string ToString()
        {
            return $"\n{base.ToString()}" +
                $"{Indent}On: {On}";
        }
    }
}