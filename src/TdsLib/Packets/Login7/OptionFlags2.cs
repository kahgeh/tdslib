using System;
using System.Collections;
using System.IO;
using TdsLib.Utility;

namespace TdsLib.Packets.Login7
{
    // public class OptionFlags2
    // {

    //     public void Read(BinaryReader reader)
    //     {
    //         _raw = reader.ReadByte();
    //         var bits = new BitArray(_raw);
    //     }
    // }

    public enum InitLanguageFailureAction
    {
        INIT_LANG_WARN = 0,
        INIT_LANG_FATAL = 1
    }


}