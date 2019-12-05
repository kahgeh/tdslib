using System;
using System.Text;
using VariableName = TdsLib.Packets.Login7.VariableNames;

namespace TdsLib.Packets.Login7
{
    public class TextVariable : Data
    {
        public VariableName Name { get; }
        public string Value { get; set; }

        public TextVariable(VariableName name) : base()
        {
            Name = name;
        }

        public override void Read(byte[] data)
        {
            var dataSpan = new Span<byte>(data);
            var relatedBytes = dataSpan.Slice(OffsetLength.Offset, OffsetLength.Length * 2);
            Value = Encoding.Unicode.GetString(relatedBytes);
        }

        public override string ToString()
        {
            return $"{Indent}{Enum.GetName(typeof(VariableNames), Name)}: {(string.IsNullOrEmpty(Value) ? "<empty>" : Value)}\n";
        }
    }
}