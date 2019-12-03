using System;
using System.Collections.Generic;
using System.IO;
using TdsLib.Utility;

namespace TdsLib.Packets.Options
{
    public class Version : Option
    {
        public byte Major { get; set; }
        public byte Minor { get; set; }
        public UInt16 Build { get; set; }
        public UInt16 SubBuild { get; set; }

        public Version()
        {
            Token = (byte)OptionToken.VERSION;
            Length = ((UInt16)(sizeof(byte) + sizeof(byte) + sizeof(UInt16) + sizeof(UInt16)));
        }

        public override byte[] GetData()
        {
            var data = new List<byte>();
            data.Add(Major);
            data.Add(Minor);
            data.AddRange(BitConverter.GetBytes(Build).Reverse());
            data.AddRange(BitConverter.GetBytes(SubBuild).Reverse());
            return data.ToArray();
        }

        public override void ReadData(BinaryReader reader)
        {
            Major = reader.ReadByte();
            Minor = reader.ReadByte();
            Build = reader.ReadReverseUInt16();
            SubBuild = reader.ReadReverseUInt16();
        }

        public override string ToString()
        {
            return $"{base.ToString()}" +
                $"{Indent}Version: {Major}.{Minor}.{Build}-{SubBuild}\n";
        }
    }
}