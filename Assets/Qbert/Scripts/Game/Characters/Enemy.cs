using System;
using UnityEngine;
using System.Collections;

public class Enemy : Character 
{
    public enum Type
    {
        RedCube,
        PurpleCube
    }
    public virtual Type typeEnemy
    {
        get { return Type.RedCube; }
    }

    public Action<Enemy> OnDestroyEvents; 

    public virtual void Init()
    {
        
    }

    public virtual void Run()
    {
        
    }

    public virtual Enemy Create(Transform root , LevelController levelController)
    {
        Transform instance = Instantiate(transform);
        instance.SetParent(root);
        instance.localPosition = Vector3.zero;
        instance.localRotation = Quaternion.identity;

        var enemy = instance.GetComponent<Enemy>();
        enemy.levelController = levelController;

        instance.gameObject.SetActive(true);

        return enemy;
    }

    protected void OnDestroyEnemy()
    {
        if (OnDestroyEvents != null)
        {
            OnDestroyEvents(this);
        }
    }
}
