using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        private MeshRenderer meshRenderer;
        private SkinManager skinManager;

        private void Awake()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();
            skinManager = SkinManager.Instance;
            cards = new List<CardAsset>();
            if (FillOnAwake)
            {
                Fill();
                if (ShuffleOnAwake) Shuffle();
            }
            else UpdateDeck();
        }

        public int Count => cards.Count;

        public CardAsset this[int i] => cards[i];

        public void Shuffle()
        {
            for (int i = 0; i < cards.Count; i++)
            {
                CardAsset c = cards[i];
                int rnd = Random.Range(0, cards.Count - 1);
                cards[i] = cards[rnd];
                cards[rnd] = c;
            }
            UpdateDeck();
        }

        public void Fill()
        {
            cards.Clear();
            CardAsset[] cardsArray = Resources.LoadAll<CardAsset>(CardsPath);
            MaxSize = cardsArray.Length;
            foreach (CardAsset card in cardsArray) cards.Add(card);
            UpdateDeck();
        }

        public CardAsset Pop()
        {
            if (cards.Count <= 0) throw new System.InvalidOperationException("Can't pop on empty deck.");
            CardAsset c = cards[cards.Count - 1];
            cards.RemoveAt(cards.Count - 1);
            UpdateDeck();
            return c;
        }

        public Card PopAndInstantiate()
        {
            Card c = Pop().InstantiateObject();
            c.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2f);
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

        public IEnumerator MoveAndPush(Card card)
        {
            card.Covered = true;
            yield return StartCoroutine(card.Move(transform, 0.5f));
            Push(card.CardAsset);
            Destroy(card.gameObject);
        }

        public void Push(CardAsset card)
        {
            cards.Add(card);
            UpdateDeck();
        }

        public void UpdateDeck()
        {
            if (cards.Count <= 0)
            {
                meshFilter.mesh = null;
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                meshFilter.mesh = skinManager.SelectedSkin.CardModel;
                Material[] materials = { skinManager.SelectedSkin.GetCardSkin(cards[0]), skinManager.SelectedSkin.CardBack };
                meshRenderer.materials = materials;
                transform.localScale = new Vector3(1f, 1f, Mathf.Lerp(1, FullScale, (float)cards.Count / MaxSize));
            }
        }
    }
}
