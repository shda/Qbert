using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoinCube : RedCube
{
    public override Type typeGameobject
    {
        get { return Type.CoinCube; }
    }

    public override GameplayObject Create(Transform root, LevelController levelController)
    {
        int maxLevel = levelController.gameField.mapGenerator.levels;

        int level = Random.Range(0, maxLevel - 1);
        int position = Random.Range(0, level);

        PositionCube positionCube = new PositionCube(level , position);

        var gaToPoint = levelController.gameplayObjects.GetGamplayObjectInPoint(positionCube);
        if (gaToPoint == null)
        {
            return SetObject(root, levelController, positionCube);
        }

        return null;
    }

    public override void Run()
    {
        StartCoroutine(WorkThead());
    }

    protected override IEnumerator WorkThead()
    {
        yield return StartCoroutine(DropToCube());
    }

    public override bool OnColisionToQbert(Qbert qbert)
    {
        if (qbert.isCheckColision)
        {
            AddCoins(ScorePrice.addCoinsToCoin);

            OnStartDestroy();
            return true;
        }

        return true;
    }

    public override bool CanJumpToMy()
    {
        return true;
    }
}
