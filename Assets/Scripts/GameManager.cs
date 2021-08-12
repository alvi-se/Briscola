using UnityEngine;
using System.Collections.Generic;
using com.alvisefavero.briscola.exceptions;
using UnityEngine.UI;

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
        public Transform RoundContainer;
        public List<Round> Rounds { get; private set; }
        public Player[] Players = new Player[2];
        public Deck MainDeck;
        public Text RoundInfo;

        public void StartGame()
        {
            if (Players.Length != 2)
                throw new PlayerNumberException("Players number must be equal to 2");
            MainDeck.Fill();
            MainDeck.Shuffle();
            for (int i = 0; i < 3; i++)
                Players[0].GiveCard(MainDeck.PopAndInstantiate(), false);

            for (int i = 0; i < 3; i++)
                Players[1].GiveCard(MainDeck.PopAndInstantiate(), true);
            Rounds = new List<Round>();
            int rdm = Mathf.RoundToInt(Random.Range(0f, 1f));
            CurrentRound = new Round(Players[rdm], OnRoundUpdate, Players[0].OnRoundUpdate, Players[1].OnRoundUpdate);
            CurrentRound.StartRound();
            Players[0].enabled = true;
            Players[1].enabled = true;
        }

        public void PlayCard(Player player, Card card)
        {
            card.transform.parent = RoundContainer;
            card.Move(player.PlayPosition, player.PlayTime, () => card.Covered = false);
            CurrentRound.AddMove(player, card.CardAsset);
        }

        public void OnRoundUpdate()
        {
            if (CurrentRound.State == Round.RoundState.ENDED)
            {
                OnRoundEnd();
                return;
            }
            RoundInfo.text = CurrentRound.CurrentPlayer.TurnString;
        }

        public void OnRoundEnd()
        {
            Player winner = CurrentRound.GetWinner();
            foreach (Card c in RoundContainer.GetComponentsInChildren<Card>())
            {
                winner.PlayerDeck.Push(c.CardAsset);
                c.Move(winner.PlayerDeck.transform, 0.5f, () => Destroy(c.gameObject));
            }

            if (MainDeck.Count <= 0)
            {
                EndGame();
                return;
            }
            bool winnerCover = winner != Players[0];
            winner.GiveCard(MainDeck.PopAndInstantiate(), winnerCover);
            System.Array.Find<Player>(Players, p => p != winner).GiveCard(MainDeck.PopAndInstantiate(), !winnerCover);
            Rounds.Add(CurrentRound);
            CurrentRound = new Round(CurrentRound.GetWinner(), CurrentRound.OnRoundUpdateCallback);
            CurrentRound.StartRound();
        }

        public void EndGame()
        {
            // TODO
        }
    }
}
