using Assets.Qbert.Scripts.GameScene.Map;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.GameAssets
{
    public class MapAsset : ScriptableObject
    {
        public int mapWidth;
        public int mapHight;
        public GameplayObjectsAsset gameplayObjectsAsset;
        public Transform[] cubePaterns;
        public CubeMap map;

        public void UpdateFromInspector()
        {
            map.UpdateFromInspector(mapWidth, mapHight);
        }
    }
}
