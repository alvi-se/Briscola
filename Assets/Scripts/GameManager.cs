using UnityEngine;
using System.Collections;
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
                throw new SingletonException("More than one instance of GameManager!");
            Instance = this;
        }
        #endregion

        public Round CurrentRound { get; private set; }
        public Transform BriscolaPosition;
        public Transform RoundContainer;
        public List<Round> Rounds { get; private set; }
        public Player[] Players = new Player[2];
        public Deck MainDeck;
        public Text RoundInfo;
        public Card Briscola { get; private set; }

        public void StartGame() => StartCoroutine(_startGame());

        private IEnumerator _startGame()
        {
            if (Players.Length != 2)
                throw new PlayerNumberException("Players number must be equal to 2");
            MainDeck.Fill();
            MainDeck.Shuffle();
            yield return StartCoroutine(GiveCards());
            Briscola = MainDeck.PopAndInstantiate();
            yield return StartCoroutine(Briscola.Move(BriscolaPosition, 0.5f, () => Briscola.Covered = false));
            Rounds = new List<Round>();
            int rdm = Mathf.RoundToInt(Random.Range(0f, 1f));
            CurrentRound = new Round(Players[rdm], OnRoundUpdate, Players[0].OnRoundUpdate, Players[1].OnRoundUpdate);
            CurrentRound.StartRound();
            Players[0].enabled = true;
            Players[1].enabled = true;
        }

        private IEnumerator GiveCards()
        {
            for (int i = 0; i < Players.Length; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Card c = MainDeck.PopAndInstantiate();
                    yield return StartCoroutine(Players[i].GiveCard(c, i == 0 ? false : true));
                }
            }
        }

        public void PlayCard(Player player, Card card)
        {
            card.transform.parent = RoundContainer;
            card.Covered = false;
            StartCoroutine(card.Move(player.PlayPosition, player.PlayTime, () => CurrentRound.AddMove(player, card.CardAsset)));
        }

        public void OnRoundUpdate()
        {
            if (CurrentRound.State == Round.RoundState.ENDED)
            {
                StartCoroutine(OnRoundEnd());
                return;
            }
            RoundInfo.text = CurrentRound.CurrentPlayer.TurnString;
        }

        public IEnumerator OnRoundEnd()
        {
            yield return new WaitForSeconds(1f);
            Player winner = CurrentRound.GetWinner();
            foreach (Card c in RoundContainer.GetComponentsInChildren<Card>())
                StartCoroutine(winner.PlayerDeck.MoveAndPush(c));

            // TODO controllare se carte sono finite e se la partita è finita

            bool winnerCover = winner != Players[0];
            yield return StartCoroutine(winner.GiveCard(MainDeck.PopAndInstantiate(), winnerCover));
            Player losingPlayer = System.Array.Find<Player>(Players, p => p != winner);
            yield return StartCoroutine(losingPlayer.GiveCard(MainDeck.PopAndInstantiate(), !winnerCover));
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
