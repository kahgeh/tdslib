using System.Collections.Generic;
using System.IO;
using TdsLib.Packets.Options;

namespace TdsLib.Packets
{
    public class OptionsMessage : PreLoginMessage
    {
        public Option[] Options { get; set; }

        public OptionsMessage()
        {

        }
        public override BinaryReader Read(BinaryReader reader)
        {
            byte token;
            var options = new List<Option>();
            while ((token = reader.ReadByte()) != (byte)OptionToken.TERMINATOR)
            {
                var option = Option.Create(token);
                option.Read(reader);
                options.Add(option);
            }
            Options = options.ToArray();

            foreach (var option in Options)
            {
                option.ReadData(reader);
            }
            return reader;
        }

        public override string ToString()
        {
            return Options.GetDisplayString();
        }
    }
}