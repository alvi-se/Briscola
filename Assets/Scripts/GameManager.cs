using UnityEngine;
using System.Collections.Generic;

namespace com.alvisefavero.briscola
{
    public class GameManager : MonoBehaviour
    {
        #region singleton
        public static GameManager Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one instance of GameManager!");
                return;
            }
            Instance = this;
        }
        #endregion

        public Round CurrentRound { get; private set; }
        public List<Round> Rounds { get; private set; }
        public Player CurrentPlayer { get; private set; }

        public Transform Player1PlayPos;
        public Transform Player2PlayPos;
        public Player[] Players = new Player[2];
        public Deck MainDeck;

        public void StartGame()
        {
            if (Players.Length != 2)
                throw new System.Exception(); // TODO
            MainDeck.Fill();
            MainDeck.Shuffle();
            for (int i = 0; i < 3; i++)
                Players[0].GiveCard(MainDeck.PopAndInstantiate());
            for (int i = 0; i < 3; i++)
                Players[1].GiveCard(MainDeck.PopAndInstantiate());
            Rounds = new List<Round>();
            CurrentRound = new Round();
            int rdm = Mathf.RoundToInt(Random.Range(0f, 1f));
            CurrentPlayer = Players[rdm];
        }

        public void OnRoundEnd()
        {
            if (MainDeck.Count <= 0)
            {
                EndGame();
                return;
            }

        }

        public void EndGame()
        {
            // TODO
        }
    }
}
