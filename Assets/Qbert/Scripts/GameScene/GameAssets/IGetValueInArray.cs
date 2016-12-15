using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Qbert.Scripts.GameScene.GameAssets;
using UnityEngine.Assertions;

public abstract class IGetValueInArray<T> :  ScriptableObject where T : class
{
    public bool isRandom = true;
    public bool isUniqueRandom = true;
    public T[] values;

    [NonSerialized]
    private T oldValue;
    [NonSerialized]
    private HashSet<T> uniqueRandomArray = new HashSet<T>();

    private T GetRandom()
    {
        T selectValue;

        if (isUniqueRandom)
        {
            if (uniqueRandomArray.Count >= values.Length)
                uniqueRandomArray.Clear();

            List<T> listRandom = new List<T>();

            foreach (var value in values)
            {
                if (uniqueRandomArray.Contains(value))
                    continue;

                listRandom.Add(value);
            }

            int random = UnityEngine.Random.Range(0, listRandom.Count);
            selectValue = listRandom[random];
            uniqueRandomArray.Add(selectValue);
        }
        else
        {
            int random = UnityEngine.Random.Range(0, values.Length);
            selectValue = values[random];
        }

        return selectValue;
    }

    private T GetNext()
    {
        if (oldValue != null)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] == oldValue)
                {
                    if (i == values.Length - 1)
                    {
                        oldValue = values.First();
                    }
                    else
                    {
                        oldValue = values[i + 1];
                    }

                    break;
                }
            }
        }
        else
        {
            oldValue = values.First();
        }

        return oldValue;
    }

    public T GetValue()
    {
        T result;

        if (isRandom)
        {
            result = GetRandom();
        }
        else
        {
            result = GetNext();
        }

        oldValue = result;

        return result;
    }

    public T GetOldValue()
    {
        if (oldValue == null)
            oldValue = GetValue();

        return oldValue;
    }
}

