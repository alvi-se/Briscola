using UnityEngine;
using com.alvisefavero.briscola.exceptions;

namespace com.alvisefavero.briscola
{
    public class Round
    {
        public class Move
        {
            public Player Player { get; private set; }
            public CardAsset Card { get; private set; }
            
            public Move(Player player, CardAsset card)
            {
                Player = player;
                Card = card;
            }
        }

        /// <summary>
        /// The index 0 is the first person to move, the index 1 the second
        /// </summary>
        public Move[] Moves { get; private set; } = new Move[2];

        public delegate void OnRoundEnd();
        public OnRoundEnd onRoundEnd;

        public void AddMove(Player player, CardAsset cardAsset)
        {
            if (Moves[0] != null && Moves[1] != null)
                throw new CompleteRoundException("Cannot add new move if both players have already played");
            Move m = new Move(player, cardAsset);
            if (Moves[0] == null)
                Moves[0] = m;
            else
            {
                Moves[1] = m;
                if (onRoundEnd != null)
                    onRoundEnd.Invoke();
            }
        }

        public Player GetWinner()
        {
            if (Moves[0] == null || Moves[1] == null)
                throw new CompleteRoundException("Can't get winner, round has not ended yet");
            if (PointsRules.CompareValues(Moves[0].Card.Value, Moves[1].Card.Value))
                return Moves[0].Player;
            else
                return Moves[1].Player;
        }
    }
}
