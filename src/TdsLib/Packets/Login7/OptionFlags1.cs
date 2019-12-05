using System;
using System.Collections;
using System.IO;
using TdsLib.Utility;

namespace TdsLib.Packets.Login7
{
    public class OptionFlags1
    {
        private string Indent { get { return "      "; } }
        public ByteOrder ByteOrder { get; set; }
        public CharType Char { get; set; }
        public FloatType Float { get; set; }
        public bool DumpLoad { get; set; }
        public bool UseDB { get; set; }
        public InitDbFailureAction InitDbFailureAction { get; set; }
        public bool SetLang { get; set; }

        private byte _raw { get; set; }
        public void Read(BinaryReader reader)
        {
            _raw = reader.ReadByte();
            var bits = new BitArray(new byte[] { _raw });

            ByteOrder = bits[0] ? ByteOrder.ORDER_68000 : ByteOrder.ORDER_X86;
            Char = (bits[1] ? CharType.CHARSET_EBCDIC : CharType.CHARSET_ASCII);
            Float = (FloatType)(new BitArray(new[] { bits[2], bits[3] })).ToByte();

            DumpLoad = bits[4];
            UseDB = bits[5];
            InitDbFailureAction = bits[6] ? InitDbFailureAction.INIT_DB_FATAL : InitDbFailureAction.INIT_DB_WARN;
            SetLang = bits[7];
        }

        public string ToString(bool raw)
        {
            if (raw)
            {
                return $"    {nameof(OptionFlags1)} : 0x{_raw.ToString("x").PadLeft(2, '0').Substring(0, 2)}\n";
            }
            return ToString();
        }

        public override string ToString()
        {
            return $"{Indent}{nameof(ByteOrder)}: {Enum.GetName(typeof(ByteOrder), ByteOrder)}\n" +
                $"{Indent}{nameof(Char)}: {Enum.GetName(typeof(CharType), Char)}\n" +
                $"{Indent}{nameof(Float)}: {Enum.GetName(typeof(FloatType), Float)}\n" +
                $"{Indent}{nameof(DumpLoad)}: {DumpLoad}\n" +
                $"{Indent}{nameof(UseDB)}: {UseDB}\n" +
                $"{Indent}{nameof(InitDbFailureAction)}: {Enum.GetName(typeof(InitDbFailureAction), InitDbFailureAction)}\n" +
                $"{Indent}{nameof(SetLang)}: {SetLang}\n";
        }
    }

    public enum ByteOrder
    {
        ORDER_X86 = 0,
        ORDER_68000 = 1
    }

    public enum CharType
    {
        CHARSET_ASCII = 0,
        CHARSET_EBCDIC = 1
    }

    public enum FloatType
    {
        FLOAT_IEEE_754 = 0,
        FLOAT_VAX = 1,
        ND5000 = 2
    }

    public enum InitDbFailureAction
    {
        INIT_DB_WARN = 0,
        INIT_DB_FATAL = 1
    }
}