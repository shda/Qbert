using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Qbert.Scripts.GameScene.Characters
{
    public class QbertModel : MonoBehaviour
    {
        public bool isFree;
        public string nameCharacter;
        public string codeName;
        public Transform booldeDead;
        public Text booleTextMesh;
        public CollisionProxy collisionProxy;

        public int coinsMultiplier;
        public int priceCoins;
        public float price;

        [Multiline]
        public string description;

        public bool isBuyed()
        {
            return GlobalValues.IsModelBuyed(codeName);
        }

        public void SetTextBooble(string text)
        {
            booleTextMesh.text = text;
        }

        void Start () 
        {
	
        }
	
        void Update () 
        {
	
        }
    }
}
