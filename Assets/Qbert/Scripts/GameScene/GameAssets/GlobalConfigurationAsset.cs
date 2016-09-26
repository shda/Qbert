using System.Linq;
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
            if (find != null)
            {
                return find;
            }
            else if(characters.Length > 0)
            {
                Debug.LogError("Set default model.");
                return characters.First();
            }

            return null;
        }
    }
}