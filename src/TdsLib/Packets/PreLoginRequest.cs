using System;
using System.IO;
using TdsLib.Errors;
using TdsLib.Utility;

namespace TdsLib.Packets
{
    public class PreLoginRequest : RequestPacket
    {
        public PreLoginMessage Message { get; set; }
        public PreLoginRequest() :
            base(PacketType.PreLogin)
        {
            LoadingProgresses.Add(nameof(ReadOptions), 0x02);
        }

        protected override IncompletePacket ReadBody(BinaryReader reader)
        {
            IncompletePacket incompletePacket;
            if ((incompletePacket = reader.ReadSection(
                    this,
                    LoadingProgress,
                    ReadOptions
                    )) != null)
            {
                return incompletePacket;
            }
            LoadingProgress = (byte)PacketState.Valid;
            return null;
        }

        private Action<BinaryReader> ReadOptions => (reader) =>
            {
                Message = new OptionsMessage();
                Message.Read(reader);
                LoadingProgress = LoadingProgresses[nameof(ReadOptions)];
            };


        public override string ToString()
        {
            return $"{base.ToString()}\n" +
                   Message.ToString();
        }
    }
}