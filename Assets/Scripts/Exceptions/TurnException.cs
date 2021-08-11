using System;

namespace com.alvisefavero.briscola
{
    public class TurnException : Exception
    {
        public Player Player { get; protected set; }
        public TurnException(Player player) : base() => Player = player;
        public TurnException(Player player, string message) : base(message)  => Player = player;
        public TurnException(Player player, string message, Exception inner) : base(message, inner)  => Player = player;
    }
}
