using System.IO;

namespace TdsLib.Packets.Options
{
    public class FedAuth : Option
    {
        public bool Required { get; set; }
        public FedAuth()
        {
            Token = (byte)OptionToken.FEDAUTHREQUIRED;
            Length = 1;
        }
        public override byte[] GetData()
        {
            return new[]{
                ((byte)(Required?1:0))};
        }

        public override void ReadData(BinaryReader reader)
        {
            Required = reader.ReadByte() == 1;
        }

        public override string ToString()
        {
            return $"{base.ToString()}" +
                $"{Indent}Required: {Required}\n";
        }
    }
}