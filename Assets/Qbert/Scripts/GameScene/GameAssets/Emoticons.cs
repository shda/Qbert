using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoticonsAsset : IGetValueInArray<string>
{
    public TextAsset textEmoticons;

    void OnValidate()
    {
        values = textEmoticons.text.Split('\n');
    }
}
