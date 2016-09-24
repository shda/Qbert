using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Qbert.Scripts.Utils
{
    public static class LinqTools 
    {
        public static T GetRandom<T>(this IEnumerable<T> array)
        {
            var tempArray = array.ToArray();
            return tempArray[Random.Range(0, tempArray.Length)];
        }

        public static List<T> Mix<T>(this IEnumerable<T> array)
        {
            var mixArray = array.ToArray();

            for (int i = 0; i < mixArray.Length; i++)
            {
                int index = Random.Range(0, mixArray.Length);

                T val = mixArray[i];
                mixArray[i] = mixArray[index];
                mixArray[index] = val;
            }

            return new List<T>(mixArray);
        }
    }
}
