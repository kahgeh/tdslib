using System.Collections.Generic;
using TdsLib.Packets.Options;
using TdsLib.StateMachine.Scaffold;

namespace TdsLib.Packets
{
    public class PreLoginResponse : Packet, IResult
    {
        public Option[] Options;
        public byte[] Data;

        public PreLoginResponse() :
            base(PacketType.PreLogin)
        {
            Option[] options = CreateOptions();

            // todo: remove hardfix
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
                    Option = EncryptionOption.ENCRYPT_NOT_SUP
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

        public override byte[] ToBytes()
        {
            var bytes = new List<byte>();
            bytes.AddRange(base.ToBytes());
            bytes.AddRange(Options.ToBytes());
            bytes.Add((byte)OptionToken.TERMINATOR);
            bytes.AddRange(Data);
            return bytes.ToArray();
        }

        public override string ToString()
        {
            return $"{base.ToString()}\n" +
                   Options.GetDisplayString();
        }
    }
}