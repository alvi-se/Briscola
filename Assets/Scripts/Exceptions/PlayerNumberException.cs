using System;

namespace com.alvisefavero.briscola.exceptions
{
    public class PlayerNumberException : Exception
    {
        public PlayerNumberException() { }
        public PlayerNumberException(string message) : base(message) { }
        public PlayerNumberException(string message, Exception inner) : base(message, inner) { }
    }
}
