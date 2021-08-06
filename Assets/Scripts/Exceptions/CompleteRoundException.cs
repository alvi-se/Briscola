using System;

namespace com.alvisefavero.briscola.exceptions
{
    public class CompleteRoundException : Exception
    {
        public CompleteRoundException() : base()
        {}

        public CompleteRoundException(string message) : base(message)
        {}

        public CompleteRoundException(string message, Exception inner) : base(message, inner)
        {}
    }
}
