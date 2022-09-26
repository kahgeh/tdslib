using System;
using System.Collections.Generic;
using TdsLib.Packets.Login7.Response;
using TdsLib.StateMachine.Scaffold;

namespace TdsLib.Packets
{
    public class LoginResponse : Packet, IResult
    {
        public Record[] Records { get; set; }
        public LoginResponse()
            : base(PacketType.TabularResult)
        {
            // todo: remove hardfix
            Type = (byte)PacketType.TabularResult;
            Status = 1;
            Channel = 0;
            PacketNumber = 1;
            Window = 0;
            Length = 0;
        }

        public override byte[] ToBytes()
        {
            var bytes = new List<byte>();
            bytes.AddRange(base.ToBytes());
            if (Records != null)
            {
                foreach (var record in Records)
                {
                    bytes.AddRange(record.ToBytes());
                }
            }
            return bytes.ToArray();
        }
    }
}