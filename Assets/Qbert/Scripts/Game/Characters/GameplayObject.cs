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
    }
    public virtual Type typeGameobject
    {
        get { return Type.RedCube; }
    }

    public Action<GameplayObject> OnDestroyEvents;

    
    public virtual void Init()
    {

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
