using System.Linq;
using Assets.Qbert.Scripts.GameScene.Characters;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.GameAssets
{
    public class GlobalConfigurationAsset : ScriptableObject
    {
        public QbertModel[] characters;
        public bool debugQuickStart = false;
        public LevelConfigAsset assetLoadLevel;
        public LevelConfigAsset assetInstruction;
        public LevelConfigAsset[] levelsAssets;

        public QbertModel GetModelByName(string name)
        {
            QbertModel find = characters.FirstOrDefault(x => x.nameCharacter == name);
            if (find == null)
            {
                if (characters.Length > 0)
                {
                    find = FindFreeModel();

                    if (find == null)
                    {
                        Debug.LogWarning("Set default model.");
                        find = characters.First(); ;
                    }
                }
                else
                {
                    Debug.LogError("Error get model.");
                }
            }

            return find;
        }

        private QbertModel FindFreeModel()
        {
            return characters.FirstOrDefault(x => x.isFree);
        }
    }
}