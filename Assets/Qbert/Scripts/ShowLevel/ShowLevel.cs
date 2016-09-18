using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowLevel : MonoBehaviour
{
    public Text textLevel;
    public Qbert qbert;
    public FadeScreen fadeScreen;
    public LevelController levelController;
    public LoadScene loadSceneAfter;
    public float waitBetweenJump = 0.5f;

    public void SetLevel()
    {
        textLevel.text = "level " + (GlobalSettings.currentLevel + 1);
    }

    void Start ()
	{
        levelController.InitLevelLoad();
        levelController.StartLoadingLevel();

	    SetLevel();

        //yield return new WaitForSeconds(0.5f);

	    fadeScreen.OnEnd = transform1 =>
	    {
            StartCoroutine(Jumper());
        };

        fadeScreen.StartDisable(1.0f);
    }


    public IEnumerator Jumper()
    {
        yield return new WaitForSeconds(1.0f);

        var logic = levelController.levelLogic;
        if (logic.type == LevelBehaviour.Type.LevelType1 ||
            logic.type == LevelBehaviour.Type.LevelType2)
        {
            yield return JumpToCube(1, 1);
            yield return JumpToCube(0, 1);
            yield return JumpToCube(1, 2);
            yield return JumpToCube(0, 1);
        }
        else if (logic.type == LevelBehaviour.Type.LevelType3 ||
            logic.type == LevelBehaviour.Type.LevelType4 ||
            logic.type == LevelBehaviour.Type.LevelType5)
        {
            yield return JumpToCube(1, 2);
            yield return JumpToCube(0, 1);
            yield return JumpToCube(1, 1);
            yield return JumpToCube(0, 1);

            yield return JumpToCube(1, 2);
            yield return JumpToCube(0, 1);
            yield return JumpToCube(1, 2);
        }

        yield return new WaitForSeconds(0.5f);

        fadeScreen.OnEnd = transform1 =>
        {
            LoadLevel();
        };

        fadeScreen.StartEnable(1.0f);
    }


    public void LoadLevel()
    {
        loadSceneAfter.OnLoadScene();
    }

    IEnumerator JumpToCube(int lint , int  pos)
    {
        bool onPress = false;

        qbert.OnEventPressCube = (character, cube) =>
        {
            onPress = true;
        };

        onPress = false;
        qbert.MoveToCube(new PositionCube(lint , pos));

        while (!onPress)
        {
            yield return null;
        }

        yield return new WaitForSeconds(waitBetweenJump);
    }
	
	void Update () 
	{
	
	}
}
