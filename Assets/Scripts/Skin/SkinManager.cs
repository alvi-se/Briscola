using UnityEngine;
using com.alvisefavero.briscola.exceptions;

namespace com.alvisefavero.briscola
{
    public class SkinManager : MonoBehaviour
    {
        #region singleton
        public static SkinManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
                throw new SingletonException("More than one instance of SkinManager!");
            Instance = this;
        }
        #endregion

        public Skin SelectedSkin;

        public void ChangeSkin(Skin skin)
        {
            SelectedSkin = skin;
            Card[] cards = FindObjectsOfType<Card>();
            foreach (Card c in cards)
            {
                c.GetComponent<MeshFilter>().mesh = SelectedSkin.CardModel;
                Material[] materials = new Material[]
                {
                    SelectedSkin.GetCardSkin(c.CardAsset),
                    SelectedSkin.CardBack
                };
                c.GetComponent<MeshRenderer>().materials = materials;
                // TODO gestione collider
            }
            Deck[] decks = FindObjectsOfType<Deck>();
            foreach (Deck d in decks)
            {
                d.GetComponent<MeshFilter>().mesh = SelectedSkin.CardModel;
                Material[] materials = new Material[]
                {
                    SelectedSkin.GetCardSkin(d[0]),
                    SelectedSkin.CardBack
                };
                d.GetComponent<MeshRenderer>().materials = materials;
                // TODO gestione collider
            }
        }
    }
}
