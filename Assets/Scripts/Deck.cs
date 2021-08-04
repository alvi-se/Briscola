using System.Collections.Generic;
using UnityEngine;

namespace com.alvisefavero.briscola
{
    public class Deck : MonoBehaviour, IInteractable
    {
        public Transform ExamplePosition;
        public float FullScale = 200f;
        [Min(0f)] public int MaxSize = 40;
        public string CardsPath = "Cards";
        public bool FillOnAwake = false;
        public bool ShuffleOnAwake = false;
        private List<CardAsset> cards;

        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;
        private SkinManager skinManager;
        new private Collider collider;

        private void Awake()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();
            collider = GetComponent<Collider>();
            skinManager = SkinManager.Instance;
            cards = new List<CardAsset>();
            if (FillOnAwake)
            {
                Fill();
                if (ShuffleOnAwake) Shuffle();
            }
            else UpdateDeck();
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
            UpdateDeck();
        }

        public void Fill()
        {
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
                collider.enabled = false;
            }
            else
            {
                meshFilter.mesh = skinManager.SelectedSkin.CardModel;
                Material[] materials = { skinManager.SelectedSkin.GetCardSkin(cards[0]), skinManager.SelectedSkin.CardBack };
                meshRenderer.materials = materials;
                transform.localScale = new Vector3(1f, 1f, Mathf.Lerp(1, FullScale, (float)cards.Count / MaxSize));
                collider.enabled = true;
            }
        }

            public void Interact()
        {
            Card c = Pop().InstantiateObject();
            c.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2f);
            c.Move(
                ExamplePosition,
                1f,
                () => c.Covered = false
            );
        }
    }
}
