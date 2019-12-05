using System.Collections.Generic;
using System.Text;

namespace TdsLib.Packets.Login7
{
    public abstract class Data
    {
        protected string Indent { get { return "    "; } }
        protected Data()
        {
            OffsetLength = new OffsetLength();
        }
        public OffsetLength OffsetLength { get; }
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