using System.Collections;
using UnityEngine;

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

        private Animator animator;
        private AudioSource audioSource;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            Covered = CoverOnAwake;
        }

        public delegate void OnMovementFinished();

        public IEnumerator Move(Transform end, float time, OnMovementFinished onMovementFinished = null, bool playSound = true)
        {
            if (playSound) audioSource.Play();
            Vector3 startPosition = transform.position;
            Quaternion startRotation = transform.rotation;
            float startTime = Time.time;
            while (Time.time < startTime + time)
            {
                transform.position = Vector3.Lerp(startPosition, end.position, (Time.time - startTime) / time);
                yield return null;
            }
            if (onMovementFinished != null) onMovementFinished.Invoke();
        }
    }
}
