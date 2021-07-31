using UnityEngine;
using Unity.Collections;
using System;

namespace com.alvisefavero.briscola
{
    [CreateAssetMenu(fileName = "New skin", menuName = "Skin")]
    public class Skin : ScriptableObject
    {
        [Serializable]
        public class CardLink
        {
            [SerializeField] private CardAsset _card;
            [SerializeField] private Material _cardFrontFace;
        }

        [SerializeField] private Material _cardBack;
        [SerializeField] private Mesh _cardModel;
        [SerializeField] private CardLink[] _cardFronts = new CardLink[40];

        private void Awake()
        {
            CardAsset[] cards = Resources.LoadAll<CardAsset>("Cards");
            foreach (CardAsset c in cards)
            {
                // TODOs
            }
        }

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
            return null;
            // TODO
        }
    }
}
