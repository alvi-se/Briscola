using UnityEngine;

namespace com.alvisefavero.briscola
{
    [CreateAssetMenu(fileName = "New card", menuName = "Card")]
    public class CardAsset : ScriptableObject
    {
        [SerializeField] private Suit _suit;
        [SerializeField][Range(1, 10)] private int _value;
        [SerializeField] private Sprite _image;
        [SerializeField] private string _cardName;
        
        public Suit Suit
        {
            get
            {
                return _suit;
            }
        }

        public int Value
        {
            get
            {
                return _value;
            }
        }

        public Sprite Image
        {
            get
            {
                return _image;
            }
        }

        public string CardName
        {
            get
            {
                return _cardName;
            }
        }
    }
}