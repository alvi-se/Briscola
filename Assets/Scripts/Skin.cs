using UnityEngine;
using System;

namespace com.alvisefavero.briscola
{
    [CreateAssetMenu(fileName = "New skin", menuName = "Skin")]
    public class Skin : ScriptableObject
    {
        [Serializable]
        private class CardLink
        {
            [SerializeField] private CardAsset _card;
            [SerializeField] private Material _cardFrontFace;
            
            public CardAsset Card
            {
                get
                {
                    return _card;
                }
            }

            public Material CardFrontFace
            {
                get
                {
                    return _cardFrontFace;
                }
            }
        }

        [SerializeField] private Material _cardBack;
        [SerializeField] private Mesh _cardModel;
        [SerializeField] private CardLink[] _cardFronts = new CardLink[40];

        public Material CardBack
        {
            get
            {
                return _cardBack;
            }
        }

        public Mesh CardModel
        {
            get
            {
                return _cardModel;
            }
        }

        public Material GetCardSkin(CardAsset card)
        {
            foreach (CardLink link in _cardFronts)
            {
                if (link.Card == card) return link.CardFrontFace;
            }
            return null;
        }
    }
}
