using System;
using System.IO;
using TdsLib.Utility;

namespace TdsLib.Packets.Options
{
    public class TraceId : Option
    {
        public byte[] Id { get; set; }
        public TraceId()
        {
            Token = (byte)OptionToken.TRACEID;
            Length = 0;
            Id = new byte[] { };
        }

        public override byte[] GetData()
        {
            return Id;
        }

        public override void ReadData(BinaryReader reader)
        {
            if (Length == 0)
            {
                return;
            }
            Id = reader.ReadBytes(Length);
        }

        public override string ToString()
        {
            return $"{base.ToString()}" +
                $"{Indent}Id: {Id.GetHexText()}\n";
        }
    }
}