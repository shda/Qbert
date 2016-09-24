using Assets.Qbert.Scripts.GameScene.Characters;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.Sound
{
    public class GameSound : MonoBehaviour
    {
        public AudioSource audioSource;

        public AudioClip jumpEnemy;
        public AudioClip jumpQbert;
        public AudioClip coinUp;
        public AudioClip qbertDown;


        public static void PlayJump(Character character)
        {
            if (character.typeObject == Character.Type.Qbert)
            {
                instance.PlaySoundShot(instance.jumpQbert);
            }
            else
            {
                instance.PlaySoundShot(instance.jumpEnemy);
            }
        }

        public static void PlayQbertDown()
        {
            instance.PlaySoundShot(instance.qbertDown);
        }

        public static void PlayCoinUp()
        {
            instance.PlaySoundShot(instance.qbertDown);
        }

        public void PlaySoundShot(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }


        void Start () 
        {
	
        }
	
        void Update () 
        {
	
        }

        private static GameSound _instance;
        public static GameSound instance
        {
            get { return _instance; }
        }

        void Awake()
        {
            _instance = this;
        }
    }
}
