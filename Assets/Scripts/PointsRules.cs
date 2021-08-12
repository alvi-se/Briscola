using System.Collections.Specialized;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace com.alvisefavero.briscola
{
    public class PointsRules
    {
        /*
        public static readonly OrderedDictionary points = new OrderedDictionary()
        {
            { 1, 11 },
            { 3, 10 },
            { 10, 4 },
            { 9, 3 },
            { 8, 2 },
            { 7, 0 },
            { 6, 0 },
            { 5, 0 },
            { 4, 0 },
            { 2, 0 }
        }.AsReadOnly();
        */

        // FIXME se possibile trovare un modo per rendere la lista immutabile
        public static readonly List<KeyValuePair<int, int>> points = new List<KeyValuePair<int, int>>
        {
            new KeyValuePair<int, int>(1, 11),
            new KeyValuePair<int, int>(3, 10),
            new KeyValuePair<int, int>(10, 4),
            new KeyValuePair<int, int>(9, 3),
            new KeyValuePair<int, int>(8, 2),
            new KeyValuePair<int, int>(7, 0),
            new KeyValuePair<int, int>(6, 0),
            new KeyValuePair<int, int>(5, 0),
            new KeyValuePair<int, int>(4, 0),
            new KeyValuePair<int, int>(2, 0)
        };

        private PointsRules()
        {}

        /// <summary>
        /// Compares the value of the two cards to decide which one wins
        /// </summary>
        /// <param name="v1">The value of the first card</param>
        /// <param name="v2">The value of the second card</param>
        /// <returns>True if the first card wins, false if the second one wins</returns>
        public static bool CompareValues(int v1, int v2)
        {
            if (v1 == v2)
                throw new ArgumentException("Cards values can't be equal");
            if (v1 <= 0 || v1 > 10 || v2 <= 0 || v2 > 10)
                throw new ArgumentOutOfRangeException("v1 and v2 must be min 1 and max 10");
            
            int v1i = points.FindIndex(pair => pair.Key == v1);
            int v2i = points.FindIndex(pair => pair.Key == v2);
            if (v1i == -1 || v2i == -1)
                throw new Exception("Points dictionary has missing cards");
            return v1i < v2i;
            
        }
    }
}
