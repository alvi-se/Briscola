using UnityEngine;

namespace com.alvisefavero.briscola
{
    public class SkinManager : MonoBehaviour
    {
        #region singleton
        private static SkinManager instance;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("More than one instance of SkinManager!");
            }
            else
                instance = this;
        }
        #endregion

        public Skin SelectedSkin;
    }
}
