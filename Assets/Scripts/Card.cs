using System.Collections;
using UnityEngine;

namespace com.alvisefavero.briscola
{
    public class Card : MonoBehaviour, IInteractable
    {
        [SerializeField] private CardAsset _cardAsset;
        public CardAsset CardAsset
        {
            get
            {
                return _cardAsset;
            }
            // Trovare soluzione per evitare l'utilizzo del setter
            set
            {
                _cardAsset = value;
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

        public void Interact()
        {
            Covered = false;
            Debug.Log("Giocato " + _cardAsset.CardName);
        }

        public delegate void OnMovementFinished();

        public void MoveCard(Vector3 position, float time, OnMovementFinished onMovementFinished = null) => StartCoroutine(_moveCard(position, time, onMovementFinished));

        private IEnumerator _moveCard(Vector3 position, float time, OnMovementFinished onMovementFinished)
        {
            Vector3 startPosition = transform.position;
            float startTime = Time.time;
            while (Time.time < startTime + time)
            {
                transform.position = Vector3.Lerp(startPosition, position, (Time.time - startTime) / +PlayTimeAnimation);
                yield return null;
            }
            if (onMovementFinished != null) onMovementFinished.Invoke();
        }
    }
}
