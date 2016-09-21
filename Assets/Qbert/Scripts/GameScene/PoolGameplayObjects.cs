using System.Collections.Generic;
using System.Linq;
using Scripts.GameScene.Characters;
using Scripts.GameScene.GameAssets;
using UnityEngine;

namespace Scripts.GameScene
{
    public class PoolGameplayObjects : MonoBehaviour
    {
        public GameplayObjectsAsset gameplayObjectsAsset;
        public Transform rootPool;

        public int initPoolSize = 10;

        public Dictionary<Character.Type , Stack<GameplayObject>> poolDictionary 
            = new Dictionary<Character.Type, Stack<GameplayObject>>();

    
        private void InitPool()
        {
            foreach (var gameplayObject in gameplayObjectsAsset.prefabs)
            {
                for (int i = 0; i < initPoolSize; i++)
                {
                    AddObjectToDict(gameplayObject);
                }
            }
        }

        private GameplayObject AddObjectToDict(GameplayObject gameplayObjectPrefab)
        {
            var createObject = Instantiate(gameplayObjectPrefab);
            GameplayObject gameplayObject = createObject.GetComponent<GameplayObject>();
  
            gameplayObject.transform.SetParent(rootPool);
            GetStack(gameplayObjectPrefab.typeObject).Push(gameplayObject);

            return gameplayObject;
        }
    
        public static GameplayObject GetGameplayObject(GameplayObject.Type type)
        {
            var hash = GetStack(type);

            GameplayObject gameplayObject = null;

            if (hash.Count > 0)
            {
                gameplayObject = hash.Pop();
            }
            else
            {
                _instance.AddObjectToDict(_instance.gameplayObjectsAsset.prefabs.First(x => x.typeObject == type));
                gameplayObject = GetGameplayObject(type);
            }

            gameplayObject.StopAllCoroutines();
            gameplayObject.transform.SetParent(null);
            // gameplayObject.gameObject.SetActive(true);
            return gameplayObject;
        }

        private static Stack<GameplayObject> GetStack(GameplayObject.Type type)
        {
            if (!_instance.poolDictionary.ContainsKey(type))
            {
                _instance.poolDictionary.Add(type, new Stack<GameplayObject>());
            }

            return _instance.poolDictionary[type];
        }

        public static void ReturnObject(GameplayObject gameplayObject)
        {
            GetStack(gameplayObject.typeObject).Push(gameplayObject);
            gameplayObject.transform.SetParent(_instance.rootPool);
        }


        private static PoolGameplayObjects _instance;
        public static PoolGameplayObjects instance
        {
            get { return _instance; }
        }

        void Awake()
        {
            _instance = this;
            rootPool.gameObject.SetActive(false);
            InitPool();
        }

        void Start () 
        {
	
        }
	
        void Update () 
        {
	
        }
    }
}
