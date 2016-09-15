using System;
using UnityEngine;
using System.Collections;

public class GameplayObject : Character 
{
    public enum Type
    {
        RedCube,
        PurpleCube,
        Transport,
        BlueCube,
        ColoredCube,
        PinkCube,
        CoinCube,
    }
    public virtual Type typeGameobject
    {
        get { return Type.RedCube; }
    }

    public int startPositionId =  -1;
    public int endPositionId =  -1;

    public Action<GameplayObject> OnDestroyEvents;

    //если на объект можно прыгнуть
    public virtual bool CanJumpToMy()
    {
        return false;
    }

    public virtual void Run()
    {

    }

    public virtual bool OnColisionToQbert(Qbert qbert)
    {
       // return true;

        if (!qbert.isCheckColision)
        {
            return true;
        }

        return false;
    }

    public virtual GameplayObject Create(Transform root , LevelController levelController)
    {
        Transform instance = Instantiate(transform);
        instance.SetParent(root);
        instance.localPosition = Vector3.zero;
        instance.localRotation = Quaternion.identity;

        var enemy = instance.GetComponent<GameplayObject>();
        enemy.levelController = levelController;

        instance.gameObject.SetActive(true);

        return enemy;
    }

    protected void OnStartDestroy()
    {
        if (OnDestroyEvents != null)
        {
            OnDestroyEvents(this);
        }
    }
}
