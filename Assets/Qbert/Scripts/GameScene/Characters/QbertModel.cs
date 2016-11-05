using System.Linq;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.Characters
{
    public class QbertModel : MonoBehaviour
    {
        public bool isFree;
        public string nameCharacter;
        public string codeName;
        public Transform booldeDead;
        public TextMesh booleTextMesh;
        public CollisionProxy collisionProxy;
        public float price;

        [Multiline]
        public string description;

        public bool isBuyed()
        {
            return GlobalValues.IsModelBuyed(codeName);
        }

        void Start () 
        {
	
        }
	
        void Update () 
        {
	
        }
    }
}
