using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundsImages : MonoBehaviour
{
    public Transform[] images;

    public void EnableImage(int index)
    {
        foreach (var image in images)
        {
            image.gameObject.SetActive(false);
        }

        images[index].gameObject.SetActive(true);
    }
}
