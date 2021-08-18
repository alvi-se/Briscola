using System.Collections.Generic;
using UnityEngine;
using com.alvisefavero.briscola.exceptions;

namespace com.alvisefavero.briscola
{
    public class LanguageManager : MonoBehaviour
    {
        public static LanguageManager Instance { get; private set; }
        
        private Dictionary<string, string> currentLanguage;

        public string DefaultLanguage = "en_us";

        private void Awake()
        {
            if (Instance != null)
                throw new SingletonException("More than one instance of LanguageManager");
            Instance = this;
            string json = Resources.Load<TextAsset>("Languages/" + DefaultLanguage + ".json").text;
            object obj = JsonUtility.FromJson(json, typeof (object));
            Debug.Log(obj.ToString());
        }


        public void Bruh()
        {
            
        }
    }
}
