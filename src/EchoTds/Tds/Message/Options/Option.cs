using System;
using System.Collections.Generic;
using System.IO;

namespace TdsLib.Message
{
    public abstract class Option : ITdsMessage
    {
        protected string Indent { get { return "    "; } }
        public byte Token { get; set; }
        public UInt16 OffSet { get; set; }
        public UInt16 Length { get; set; }

        public virtual byte[] ToBytes()
        {
            var bytes = new List<byte>();
            bytes.Add(Token);
            bytes.AddRange(BitConverter.GetBytes(OffSet).Reverse());
            bytes.AddRange(BitConverter.GetBytes(Length).Reverse());
            return bytes.ToArray();
        }

        public abstract byte[] GetData();
        public abstract void ReadData(BinaryReader reader);

        public static UInt16 GetOptionSize()
        {
            return sizeof(byte) + sizeof(UInt16) + sizeof(UInt16);
        }

        public void Read(BinaryReader reader)
        {
            OffSet = reader.ReadReverseUInt16();
            Length = reader.ReadReverseUInt16();
        }

        public static Option Create(byte byteToken)
        {
            var token = (OptionToken)byteToken;
            switch (token)
            {
                case OptionToken.ENCRYPTION:
                    return new Options.Encryption();

                case OptionToken.FEDAUTHREQUIRED:
                    return new Options.FedAuth();

                case OptionToken.INSTOPT:
                    return new Options.InstOpt();

                case OptionToken.MARS:
                    return new Options.MultipleActiveResultSets();

                case OptionToken.THREADID:
                    return new Options.ThreadId();

                case OptionToken.TRACEID:
                    return new Options.TraceId();

                case OptionToken.VERSION:
                    return new Options.Version();
            }

            throw new Exception($"option token in prelogin message {token} is not valid");
        }

        public override string ToString()
        {
            return $"  Option - {Enum.GetName(typeof(OptionToken), Token)}\n" +
              $"{Indent}Token: {Token}\n" +
              $"{Indent}OffSet: {OffSet}\n" +
              $"{Indent}Length: {Length}\n";
        }
    }
}