using UnityEngine;

namespace com.alvisefavero.briscola
{
    public class PlayerController : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Card card = hit.transform.GetComponent<Card>();
                    if (card != null)
                    {
                        card.Play();
                    }
                }
            }
        }
    }
}