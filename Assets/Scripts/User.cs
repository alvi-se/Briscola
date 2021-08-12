using UnityEngine;

namespace com.alvisefavero.briscola
{
    public class User : Player
    {
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (GameManager.Instance.CurrentRound.CurrentPlayer != this) return;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Card c = hit.transform.GetComponent<Card>();
                    if (c == null ||
                        hit.transform.parent != HandTransform
                    )
                        return;
                    PlayCard(c);
                }
            }
        }
    }
}
