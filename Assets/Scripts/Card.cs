using UnityEngine;

namespace com.alvisefavero.briscola
{
    public class Card : MonoBehaviour
    {
        public CardAsset cardAsset { get; private set; }

        [SerializeField] private bool _covered = false;
    
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

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
    }
}
