using System;
using System.IO;

namespace TdsLib.Message
{
    public class TdsStream
    {
        public static Message Read(BinaryReader reader)
        {
            var type = (MessageType)reader.ReadByte();
            var message = CreateMessage(type);
            message.Load(reader, 0);
            return message;
        }

        private static Message CreateMessage(MessageType type)
        {
            switch (type)
            {
                case MessageType.PreLogin:
                    return new PreLoginRequest();
                default:
                    throw new System.Exception("Type unexpected");
            }
        }
    }
}