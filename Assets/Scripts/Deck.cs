using System.Collections.Generic;
using UnityEngine;

namespace com.alvisefavero.briscola
{
    public class Deck : MonoBehaviour
    {
        public float FullScale = 200f;
        [Min(0f)] public int MaxSize = 40;
        public string CardsPath = "Cards";
        public bool FillOnAwake = false;
        public bool ShuffleOnAwake = false;
        private List<CardAsset> cards;

        private MeshFilter meshFilter;

        private void Awake()
        {
            meshFilter = GetComponent<MeshFilter>();
            cards = new List<CardAsset>();
            if (FillOnAwake)
            {
                Fill();
                if (ShuffleOnAwake) Shuffle();
            }
        }

        public void Shuffle()
        {
            for (int i = 0; i < cards.Count; i++)
            {
                CardAsset c = cards[i];
                int rnd = Random.Range(0, cards.Count - 1);
                cards[i] = cards[rnd];
                cards[rnd] = c;
            }
        }

        public void Fill()
        {
            CardAsset[] cardsArray = Resources.LoadAll<CardAsset>(CardsPath);
            foreach (CardAsset card in cardsArray) cards.Add(card);
        }

        public CardAsset Pop()
        {
            if (cards.Count <= 0) throw new System.InvalidOperationException("Can't pop on empty deck.");
            CardAsset c = cards[cards.Count - 1];
            cards.RemoveAt(cards.Count - 1);
            return c;
        }

        public bool TryPop(out CardAsset card)
        {
            try
            {
                card = Pop();
                return true;
            }
            catch (System.InvalidOperationException)
            {
                card = null;
                return false;
            }
        }

        public void Push(CardAsset card)
        {
            cards.Add(card);
        }

        public void OnDeckChanged()
        {
            if (cards.Count <= 0)
            {
                // Stuff
            }
        }
    }
}
