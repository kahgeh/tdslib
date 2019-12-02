using System.IO;

namespace TdsLib.Packets.Options
{
    public class MultipleActiveResultSets : Option
    {
        public bool Enabled { get; set; }
        public MultipleActiveResultSets()
        {
            Token = (byte)OptionToken.MARS;
            Length = sizeof(byte);
        }

        public override byte[] GetData()
        {
            return new[]{
                ((byte)(Enabled?1:0))
            };
        }

        public override void ReadData(BinaryReader reader)
        {
            Enabled = reader.ReadByte() == 1;
        }

        public override string ToString()
        {
            return $"\n{base.ToString()}" +
                $"{Indent}Enabled: {Enabled}";
        }
    }
}