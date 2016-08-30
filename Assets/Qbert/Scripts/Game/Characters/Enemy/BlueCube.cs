using UnityEngine;
using System.Collections;

public class BlueCube : RedCube
{
    [Header("BlueCube")] public float timeStopDuration = 2.0f;

    public override Type typeGameobject
    {
        get { return Type.BlueCube; }
    }

    public override bool OnColisionToQbert(Qbert qbert)
    {
        if (qbert.isCheckColision)
        {
            AddScore(ScorePrice.getClueCube);

            levelController.StartPauseGameObjectsToSecond(timeStopDuration);
            OnStartDestroy();
            return true;
        }

        return true;
    }
}


