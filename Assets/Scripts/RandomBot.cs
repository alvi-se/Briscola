using UnityEngine;

namespace com.alvisefavero.briscola
{
    /// <summary>
    /// Not an AI, it simply chooses a random card to play
    /// </summary>
    public class RandomBot : Player
    {
        public override void OnRoundUpdate()
        {
            base.OnRoundUpdate();
            if (GameManager.Instance.CurrentRound.CurrentPlayer == this)
                PlayCard(Mathf.RoundToInt(Random.Range(0, 2)));
        }
    }
}
