using System;
using System.IO;

namespace TdsLib.Message
{
    public class PreLoginRequest : Message
    {
        PreLoginMessage Message { get; set; }
        public PreLoginRequest() :
            base(MessageType.PreLogin)
        {

        }

        public override void Load(BinaryReader reader, int callIndex)
        {
            ReadHeader(reader);
            if (callIndex == 0)
            {
                Message = new OptionsMessage();
                Message.Read(reader);
            }

        }

        public override string ToString()
        {
            return $"{base.ToString()}\n" +
                   Message.ToString();
        }
    }
}