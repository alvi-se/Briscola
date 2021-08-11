using com.alvisefavero.briscola.exceptions;
using UnityEngine;

namespace com.alvisefavero.briscola
{
    public class Round
    {
        #region round classes
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

    public enum RoundState
    {
        ONGOING,
        ENDED
    }
    #endregion

        /// <summary>
        /// The index 0 is the first person to move, the index 1 the second
        /// </summary>
        public Move[] Moves { get; private set; }
        public Player CurrentPlayer { get; private set; }
        public RoundState State { get; private set; }
        public delegate void OnRoundUpdate();
        public OnRoundUpdate OnRoundUpdateCallback { get; private set; }

        public Round(Player startingPlayer, params OnRoundUpdate[] onRoundUpdateCallback)
        {
            Moves = new Move[2];
            State = RoundState.ONGOING;
            CurrentPlayer = startingPlayer;
            foreach (OnRoundUpdate onRoundUpdate in onRoundUpdateCallback)
                OnRoundUpdateCallback += onRoundUpdate;
        }

        public void AddMove(Player player, CardAsset cardAsset)
        {
            if (player != CurrentPlayer)
                throw new TurnException(player, player.ToString() + " tried to play but it's not his turn");
                
            if (Moves[0] != null && Moves[1] != null)
                throw new RoundException(this, "Cannot add new move if both players have already played");
            Move m = new Move(player, cardAsset);
            if (Moves[0] == null)
            {
                Moves[0] = m;
                CurrentPlayer = System.Array.Find<Player>(GameManager.Instance.Players, p => p != CurrentPlayer);
            }
            else
            {
                Moves[1] = m;
                State = RoundState.ENDED;
            }
            if (OnRoundUpdateCallback != null)
                OnRoundUpdateCallback.Invoke();
        }

        public Player GetWinner()
        {
            if (State == RoundState.ONGOING)
                throw new RoundException(this, "Can't get winner, round has not ended yet");
            if (PointsRules.CompareValues(Moves[0].Card.Value, Moves[1].Card.Value))
                return Moves[0].Player;
            else
                return Moves[1].Player;
        }
    }
}
