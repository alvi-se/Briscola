using UnityEngine;

namespace com.alvisefavero.briscola
{
    public class GameManager : MonoBehaviour
    {
        #region singleton
        public static GameManager Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one instance of GameManager!");
                return;
            }
            Instance = this;
        }
        #endregion

        public Transform player1PlayPos;
        public Transform player2PlayPos;
    }
}
