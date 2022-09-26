using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TdsLib.Packets.Login7.Response
{
    public class LoginAck : Record
    {
        public byte Interface { get; set; }
        public UInt32 TdsVersion { get; set; }
        public string ProgramName { get; set; }
        public byte MajorVersion { get; set; }
        public byte MinorVersion { get; set; }
        public byte BuildNumberHighByte { get; set; }
        public byte BuildNumberLowByte { get; set; }
        public LoginAck() : base(TokenType.LoginAck)
        {
            Interface = 0x01;
            TdsVersion = 0x72090002;
            ProgramName = "Microsoft SQL Server";
            MajorVersion = 0x00;
            MinorVersion = 0x00;
            BuildNumberHighByte = 0x00;
            BuildNumberLowByte = 0x00;

            Length = GetSize();
        }

        public override byte[] ToBytes()
        {
            var bytes = new List<byte>();
            bytes.AddRange(base.ToBytes());
            bytes.Add(Interface);
            bytes.AddRange(BitConverter.GetBytes(TdsVersion).Reverse());
            bytes.AddRange(Encoding.Unicode.GetBytes(ProgramName));
            bytes.Add(0x00);
            bytes.Add(0x00);
            bytes.Add(MajorVersion);
            bytes.Add(MinorVersion);
            bytes.Add(BuildNumberHighByte);
            bytes.Add(BuildNumberLowByte);
            return bytes.ToArray();
        }

    }
}