using UnityEngine;

namespace com.alvisefavero.briscola
{
    public class SkinManager : MonoBehaviour
    {
        #region singleton
        public static SkinManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one instance of SkinManager!");
            }
            else
                Instance = this;
        }
        #endregion

        public Skin SelectedSkin;
    }
}
