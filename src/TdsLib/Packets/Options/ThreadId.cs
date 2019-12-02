using System;
using System.IO;
using TdsLib.Utility;

namespace TdsLib.Packets.Options
{
    public class ThreadId : Option
    {
        public UInt32? Id { get; set; }
        public ThreadId()
        {
            Token = (byte)OptionToken.THREADID;
            Length = 0;
        }

        public override byte[] GetData()
        {
            if (!Id.HasValue)
            {
                return new byte[] { };
            }
            return BitConverter.GetBytes(Id.Value).Reverse();
        }

        public override void ReadData(BinaryReader reader)
        {
            if (Length == 0)
            {
                return;
            }
            Id = reader.ReadReverseUInt32();
        }

        public override string ToString()
        {
            return $"\n{base.ToString()}" +
                $"{Indent}Id: {Id}";
        }
    }
}