using System;

namespace com.alvisefavero.briscola.exceptions
{
    public class TooCardsException : System.Exception
    {
        public TooCardsException(string message, Exception inner) : base(message, inner)
        {}
        public TooCardsException(string message) : base(message)
        {}

        public TooCardsException() : base()
        {}
    }
}