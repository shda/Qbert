using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Assertions;

public class ColorsAsset : IGetValueInArray<ColorsAsset.Colors>
{
    [Serializable]
    public class Colors
    {
        public Color[] colors;
    }
}