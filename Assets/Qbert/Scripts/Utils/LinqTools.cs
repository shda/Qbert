using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class LinqTools 
{
    public static T GetRandom<T>(this IEnumerable<T> array)
    {
        var tempArray = array.ToArray();
        return tempArray[Random.Range(0, tempArray.Length)];
    }
}
