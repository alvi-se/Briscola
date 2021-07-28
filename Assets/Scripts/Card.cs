using UnityEngine;
using System.Collections;

namespace com.alvisefavero.briscola
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private CardAsset _cardAsset;
        public CardAsset CardAsset
        {
            get
            {
                return _cardAsset;
            }
        }

        public bool CoverOnAwake = true;

        private bool _covered;
    
        public bool Covered
        {
            get
            {
                return _covered;
            }

            set
            {
                _covered = value;
                animator.SetBool("Covered", _covered);
            }
        }
        public float PlayTimeAnimation = 1f;

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            Covered = CoverOnAwake;
        }

        public void Play()
        {
            // transform.position = GameManager.Instance.player1PlayPos.position;
            StartCoroutine(PlayAnimation());
            Covered = false;
            Debug.Log("Giocato " + _cardAsset.CardName);
        }

        private IEnumerator PlayAnimation()
        {
            Vector3 startPosition = transform.position;
            float startTime = Time.time;
            while (Time.time < startTime + PlayTimeAnimation)
            {
                transform.position = Vector3.Lerp(startPosition, GameManager.Instance.player1PlayPos.position, (Time.time - startTime) / + PlayTimeAnimation);
                yield return null;
            }
        }
    }
}
