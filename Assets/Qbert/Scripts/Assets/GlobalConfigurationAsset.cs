using UnityEngine;
using System.Collections;

public class GlobalConfigurationAsset : ScriptableObject
{
    public bool debugQuickStart = false;
    public LevelConfigAsset assetLoadLevel;
    public LevelConfigAsset[] levelsAssets;
}