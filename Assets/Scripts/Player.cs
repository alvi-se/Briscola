using UnityEngine;
using com.alvisefavero.briscola.exceptions;
using System;
using System.Collections;

namespace com.alvisefavero.briscola
{
    public abstract class Player : MonoBehaviour
    {
        public Transform[] CardsPositions = new Transform[3];
        public Card[] Hand { get; private set; } = new Card[3];
        public Transform PlayPosition;
        public Deck PlayerDeck;
        public float PlayTime = 0.5f;
        public string TurnString;
        public string Name;
        private GameManager gameManager;
        [SerializeField] private Transform _handTransform;

        public Transform HandTransform
        {
            get
            {
                return _handTransform;
            }
        }

        private void Awake()
        {
            gameManager = GameManager.Instance;
        }

        public IEnumerator GiveCard(Card card, bool covered)
        {
            card.transform.SetParent(_handTransform);
            for (int i = 0; i < Hand.Length; i++)
            {
                if (Hand[i] == null)
                {
                    Hand[i] = card;
                    yield return StartCoroutine(card.Move(CardsPositions[i], 0.5f, () => card.Covered = covered));
                    yield break;
                }
            }
            throw new TooCardsException("Hand is full");
            // FIXME mettere carta in prima posizione libera
            // card.Move(CardsPositions[_hand.childCount - 1], 0.5f, () => card.Covered = covered);
        }
        public void GiveCard(Card card, Card.OnMovementFinished onMovementFinished)
        {
            card.transform.SetParent(_handTransform);
            for (int i = 0; i < Hand.Length; i++)
            {
                if (Hand[i] == null)
                {
                    Hand[i] = card;
                    card.Move(CardsPositions[i], 0.5f, onMovementFinished);
                    return;
                }
            }
            throw new TooCardsException("Hand is full");
        }

        public void PlayCard(int n)
        {
            // TODO controllare n
            Card c = Hand[n];
            Hand[n] = null;
            gameManager.PlayCard(this, c);
        }

        // FIXME broken
        public void PlayCard(CardAsset cardAsset)
        {
            // TODO controllare cardAsset
            int cardIndex = Array.FindIndex<Card>(Hand, c => c.CardAsset = cardAsset);
            PlayCard(cardIndex);
        }

        public void PlayCard(Card card)
        {
            // TODO controllare carta
            int cardIndex = Array.FindIndex<Card>(Hand, c => c == card);
            PlayCard(cardIndex);
        }

        public virtual void OnRoundUpdate()
        {}
    }
}
