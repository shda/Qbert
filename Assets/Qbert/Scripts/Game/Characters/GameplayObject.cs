using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameplayObject : Character 
{
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
        if (!qbert.isCheckColision)
        {
            return true;
        }

        return false;
    }

    public virtual GameplayObject TryInitializeObject(Transform root , LevelController levelController)
    {
        transform.SetParent(root);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        var enemy = transform.GetComponent<GameplayObject>();
        enemy.levelController = levelController;

        transform.gameObject.SetActive(true);

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
