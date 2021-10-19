using System.Collections.Generic;
using UnityEngine;
using com.alvisefavero.collections;
using System;

namespace com.alvisefavero.briscola
{
    [Serializable]
    public class CircularListUnity<T> : CircularList<T>, ISerializationCallbackReceiver
    {
        public List<T> SerializedList;

        public void OnBeforeSerialize()
        {
            SerializedList = new List<T>();
            foreach (T element in this)
                SerializedList.Add(element);
        }
        
        public void OnAfterDeserialize()
        {
            Clear();
            if (SerializedList != null)
                foreach (T element in SerializedList)
                    Add(element);
        }
    }
}
