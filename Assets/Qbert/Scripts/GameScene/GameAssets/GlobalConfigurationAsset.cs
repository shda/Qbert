using UnityEngine;

namespace Scripts.GameScene.GameAssets
{
    public class GlobalConfigurationAsset : ScriptableObject
    {
        public bool debugQuickStart = false;
        public LevelConfigAsset assetLoadLevel;
        public LevelConfigAsset assetInstruction;
        public LevelConfigAsset[] levelsAssets;
    }
}