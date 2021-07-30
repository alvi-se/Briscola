using UnityEngine;

namespace com.alvisefavero.briscola
{
    [CreateAssetMenu(fileName = "New deck", menuName = "Deck")]
    public class DeckAsset : ScriptableObject
    {
        [SerializeField] private Sprite _cardBack;
        [SerializeField] private SerializableDictionary<CardAsset, Sprite> _pairs;

        public Sprite CardBack
        {
            get
            {
                return _cardBack;
            }
        }

        public Sprite GetCardSkin(CardAsset card)
        {
            return _pairs[card];
        }
    }
}
