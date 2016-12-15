using UnityEngine;
using Assets.Qbert.Scripts.GameScene.GameAssets;


public abstract class IGetMap : ScriptableObject
{
    public abstract MapAsset GetMap();
}
