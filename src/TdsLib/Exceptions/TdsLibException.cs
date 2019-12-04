using System;
using TdsLib.Errors;

namespace TdsLib.Exceptions
{
    public class TdsLibException : Exception
    {
        public TdsLibError TdsLibError { get; }
        public TdsLibException(string message) : base(message) { }

        public TdsLibException(TdsLibError tdsLibError)
        : base(tdsLibError.Message)
        {
            TdsLibError = tdsLibError;
        }
    }
}