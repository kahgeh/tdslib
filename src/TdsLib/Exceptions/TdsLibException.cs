using System;

namespace TdsLib.Exceptions
{
    public class TdsLibException : Exception
    {
        public TdsLibException(string message) : base(message) { }
    }
}