using System;

namespace com.alvisefavero.briscola.exceptions
{
    public class RoundException : Exception
    {
        public Round Round { get; protected set; }

        public RoundException(Round round) => Round = round;

        public RoundException(Round round, string message) : base(message)  => Round = round;

        public RoundException(Round round, string message, Exception inner) : base(message, inner)  => Round = round;
    }
}
