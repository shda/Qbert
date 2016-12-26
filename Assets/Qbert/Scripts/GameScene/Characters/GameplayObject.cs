using System;
using Assets.Qbert.Scripts.GameScene.Levels;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.Characters
{
    public class GameplayObject : Character 
    {
        public Action<GameplayObject> OnDestroyEvents;

        //если на объект можно прыгнуть
        public virtual bool CanJumpToMy
        {
            get { return false; }
            
        }
        //обозначить место падения
        public virtual bool IsFlashPlaceDownObject
        {
            get { return true; }
        }

        public virtual bool OnProcessingQbertCollision(Qbert qbert)
        {
            if (qbert.checkCollision == CollisionCheck.All)
            {
                return true;
            }

            return false;
        }

        public virtual GameplayObject TryInitializeObject(Transform root , LevelController levelController)
        {
            transform.SetParent(root);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            var enemy = transform.GetComponent<GameplayObject>();
            enemy.levelController = levelController;

            transform.gameObject.SetActive(true);

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
}
