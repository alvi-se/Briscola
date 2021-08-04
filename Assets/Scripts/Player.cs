using UnityEngine;
using com.alvisefavero.briscola.exceptions;

namespace com.alvisefavero.briscola
{
    public class Player : MonoBehaviour
    {
        public Transform[] CardsPositions = new Transform[3];
        [SerializeField] private Transform _hand;

        public Transform Hand
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

        public void GiveCard(Card card)
        {
            if (_hand.childCount >= 3)
                throw new TooCardsException("Hand must have at max 3 cards");
            card.transform.SetParent(_hand);
            card.Move(CardsPositions[_hand.childCount - 1], 0.5f, () => card.Covered = false);
        }
    }
}
