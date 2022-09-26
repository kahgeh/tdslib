using System.Collections.Generic;
using System.Text;
using VariableName = TdsLib.Packets.Login7.VariableNames;

namespace TdsLib.Packets.Login7
{
    public abstract class Data
    {
        protected string Indent { get { return "    "; } }
        protected Data(VariableName name)
        {
            Name = name;
            OffsetLength = new OffsetLength();
        }
        public OffsetLength OffsetLength { get; }
        public VariableName Name { get; }
        public abstract void Read(byte[] data);
    }

    public static class TextVariablesExtensions
    {
        public static string GetDisplayText(this IEnumerable<Data> data)
        {
            var builder = new StringBuilder();
            foreach (var datum in data)
            {
                builder.Append(datum.ToString());
            }
            return builder.ToString();
        }
    }
}