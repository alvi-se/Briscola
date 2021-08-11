using UnityEngine;
using com.alvisefavero.briscola.exceptions;
using System;

namespace com.alvisefavero.briscola
{
    public abstract class Player : MonoBehaviour
    {
        public Transform[] CardsPositions = new Transform[3];
        public Transform PlayPosition;
        public Deck PlayerDeck;
        public float PlayTime = 0.5f;
        public string TurnString;
        private GameManager gameManager;
        [SerializeField] private Transform _hand;

        public Transform Hand
        {
            get
            {
                return _hand;
            }
        }

        private void Awake()
        {
            gameManager = GameManager.Instance;
        }

        public void GiveCard(Card card, bool covered)
        {
            if (_hand.childCount >= 3)
                throw new TooCardsException("Hand must have at max 3 cards");
            card.transform.SetParent(_hand);
            // FIXME mettere carta in prima posizione libera
            card.Move(CardsPositions[_hand.childCount - 1], 0.5f, () => card.Covered = covered);
        }

        public void PlayCard(int n)
        {
            // TODO controllare n
            Card card = _hand.GetComponentsInChildren<Card>()[n];
            gameManager.PlayCard(this, card);
        }

        public void PlayCard(CardAsset cardAsset)
        {
            // TODO controllare cardAsset
            Card card = Array.Find<Card>(_hand.GetComponentsInChildren<Card>(), c => c.CardAsset = cardAsset);
            card.transform.parent = null;
            card.Move(PlayPosition, PlayTime, () => card.Covered = false);
        }

        public virtual void OnRoundUpdate()
        {}
    }
}
