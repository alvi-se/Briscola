using System.Collections.Generic;
using UnityEngine;

namespace com.alvisefavero.briscola
{
    public class Deck : MonoBehaviour
    {
        public string CardsPath = "Cards";
        private Stack<CardAsset> cards;

        private void Awake()
        {
            cards = new Stack<CardAsset>();
            CardAsset[] cardsArray = Resources.LoadAll<CardAsset>(CardsPath);
            foreach (CardAsset card in cardsArray) cards.Push(card);
        }

        public void Shuffle()
        {
            // TODO
        }
    }
}
