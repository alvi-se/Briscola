using UnityEngine;
using com.alvisefavero.briscola.exceptions;

namespace com.alvisefavero.briscola
{
    public class TextManager : MonoBehaviour
    {
        #region singleton
        
        public static TextManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
                throw new SingletonException("More than one instance of TextManager");
            Instance = this;
        }

        #endregion

        
    }
}
