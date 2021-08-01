using System.Collections.Generic;
using UnityEngine;

namespace com.alvisefavero.briscola
{
    public class Deck : MonoBehaviour, IInteractable
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
            else OnDeckChanged();
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
            OnDeckChanged();
        }

        public void Fill()
        {
            CardAsset[] cardsArray = Resources.LoadAll<CardAsset>(CardsPath);
            MaxSize = cardsArray.Length;
            foreach (CardAsset card in cardsArray) cards.Add(card);
            OnDeckChanged();
        }

        public CardAsset Pop()
        {
            if (cards.Count <= 0) throw new System.InvalidOperationException("Can't pop on empty deck.");
            CardAsset c = cards[cards.Count - 1];
            cards.RemoveAt(cards.Count - 1);
            OnDeckChanged();
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
            OnDeckChanged();
        }

        public void OnDeckChanged()
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
                transform.localScale = new Vector3(1f, 1f, Mathf.Lerp(1, FullScale, (float) cards.Count / MaxSize));
                collider.enabled = true;
            }
        }

        public void Interact()
        {
            Pop();
        }
    }
}
