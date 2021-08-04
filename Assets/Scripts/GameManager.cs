using UnityEngine;

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

        private void Start()
        {
            StartGame();
        }

        public Transform player1PlayPos;
        public Transform player2PlayPos;
        public Player player;
        public Deck MainDeck;

        public void StartGame()
        {
            MainDeck.Fill();
            MainDeck.Shuffle();
            for (int i = 0; i < 3; i++)
                player.GiveCard(MainDeck.PopAndInstantiate());
        }
    }
}
