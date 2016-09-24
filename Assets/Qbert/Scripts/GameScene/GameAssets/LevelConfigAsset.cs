using Assets.Qbert.Scripts.GameScene.Levels;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.GameAssets
{
    public class LevelConfigAsset : ScriptableObject
    {
        [Header("Карта по умолчанию")]
        public MapAsset globalMap;

        [Header("Тип уровня")]
        public LevelLogic.Type typeLevel;

        [Header("Цвета по умолчанию")]
        public Color[] globalLevelColors;

        public Round[] rounds;
    }
}
