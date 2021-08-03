using UnityEngine;

namespace com.alvisefavero.briscola
{
    public class Player : MonoBehaviour
    {
        public Transform[] CardsPositions = new Transform[3];
        [SerializeField] private Card[] _hand = new Card[3];

        public Card[] Hand
        {
            get
            {
                return _hand;
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    IInteractable interactable = hit.transform.GetComponent<IInteractable>();
                    if (interactable != null)
                    {
                        interactable.Interact();
                    }
                }
            }
        }
    }
}
