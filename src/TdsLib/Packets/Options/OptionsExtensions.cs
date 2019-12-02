using System;
using System.Collections.Generic;
using System.Text;

namespace TdsLib.Packets.Options
{
    public static class OptionsExtensions
    {
        public static byte[] ToBytes(this Option[] options)
        {
            var bytes = new List<byte>();
            foreach (var option in options)
            {
                bytes.AddRange(option.ToBytes());
            }
            return bytes.ToArray();
        }

        public static UInt16 GetSize(this Option[] options)
        {
            return (UInt16)(options.Length * Option.GetOptionSize());
        }

        public static byte[] GetData(this Option[] options)
        {
            var data = new List<byte>();
            foreach (var option in options)
            {
                data.AddRange(option.GetData());
            }
            return data.ToArray();
        }

        public static Option[] SetOffSet(this Option[] options)
        {
            const UInt16 TerminatorSize = 1;
            var optionsSize = (UInt16)((options.Length * Option.GetOptionSize()) + TerminatorSize);
            var offSet = optionsSize;
            foreach (var option in options)
            {
                option.OffSet = offSet;
                offSet = (UInt16)(offSet + option.Length);
            }
            return options;
        }

        public static string GetDisplayString(this Option[] options)
        {
            var builder = new StringBuilder();
            foreach (var option in options)
            {
                builder.Append(option.ToString());
            }
            return builder.ToString();
        }
    }
}