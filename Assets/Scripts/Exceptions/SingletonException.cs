using System;

namespace com.alvisefavero.briscola.exceptions
{
    public class SingletonException : Exception
    {
        public SingletonException() {}
        public SingletonException(string message) : base(message) {}
        public SingletonException(string message, System.Exception inner) : base(message, inner) {}
    }
}
