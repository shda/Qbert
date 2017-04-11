using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScene : MonoBehaviour
{
    public FallowToCameraCharacter fallowToCameraCharacter;

    void Awake()
    {
        fallowToCameraCharacter.point = MainCamera.root;
    }
	
	void Update () 
	{
		
	}
}
