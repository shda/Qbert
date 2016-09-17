using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public GameFieldGenerator gameFieldGenerator;
    public Qbert qbert;
    public MapAsset mapLevelLoad;
    public Game game;

    void Start () 
	{
        gameFieldGenerator.mapAsset = mapLevelLoad;
        gameFieldGenerator.CreateMap();

        game.StartGame();
    }
	
	void Update () 
	{
	
	}
}
