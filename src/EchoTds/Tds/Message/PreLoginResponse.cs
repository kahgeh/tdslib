using System;
using System.Collections.Generic;
using TdsLib.Message.Options;

namespace TdsLib.Message
{
    public class PreLoginResponse : ITdsMessage
    {
        public byte Type { get; set; }
        public byte Status { get; set; }
        public UInt16 Length { get; set; }
        public UInt16 Channel { get; set; }
        public byte PacketNumber { get; set; }
        public byte Window { get; set; }
        public Option[] Options;
        public byte[] Data;

        public PreLoginResponse()
        {
            Option[] options = CreateOptions();

            Type = 4;
            Status = 1;
            Channel = 0;
            PacketNumber = 1;
            Window = 0;
            Options = options;
            Data = options
                    .SetOffSet()
                    .GetData();
            Length = 54;
        }

        private static Option[] CreateOptions()
        {
            return new Option[]{
                new Options.Version
                {
                    Major = 15,
                    Minor = 0,
                    Build = 2070,
                    SubBuild = 0
                },
                new Options.Encryption
                {
                    On = false
                },
                new Options.InstOpt
                {
                    ServerName = string.Empty
                },
                new Options.ThreadId(),
                new Options.MultipleActiveResultSets{
                    Enabled = false
                },
                new Options.TraceId(),
                new Options.FedAuth{
                    Required = false
                }
            };
        }

        public byte[] ToBytes()
        {
            var bytes = new List<byte>();
            bytes.Add(Type);
            bytes.Add(Status);
            bytes.AddRange(BitConverter.GetBytes(Length).Reverse());
            bytes.AddRange(BitConverter.GetBytes(Channel).Reverse());
            bytes.Add(PacketNumber);
            bytes.Add(Window);
            bytes.AddRange(Options.ToBytes());
            bytes.Add((byte)OptionToken.TERMINATOR);
            bytes.AddRange(Data);
            return bytes.ToArray();
        }
    }
}