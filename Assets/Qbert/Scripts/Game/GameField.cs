using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameField : MonoBehaviour
{
    public GameFieldGenerator mapGenerator;

    public Cube[] field; 

    void Awake()
    {
        ParseMap();
    }

    private void ParseMap()
    {
        field = mapGenerator.GetComponentsInChildren<Cube>();
    }

    public Cube GetCube(int line, int number)
    {
        return field.FirstOrDefault(x => x.lineNumber == line && x.numberInLine == number);
    }

	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}
}
