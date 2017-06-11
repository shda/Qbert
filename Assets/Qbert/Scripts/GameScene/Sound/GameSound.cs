using Assets.Qbert.Scripts.GameScene.Characters;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.Sound
{
    public class GameSound : MonoBehaviour
    {
        public AudioSource audioSource;

        [Header("Character sounds")]
        public AudioClip jumpEnemy;
        public AudioClip jumpQbert;
        public AudioClip qbertDown;

       
        [Header("Start timer sounds")]
        public AudioClip levelTimer;
        public AudioClip levelStartLabel;

        [Header("Level sounds")]
        public AudioClip coinUp;
        public AudioClip winLevel;
        public AudioClip levelLose;

        public AudioClip getColoredCube;
        public AudioClip getBlueCube;


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

        public void PlaySoundShot(AudioClip clip)
        {
            if (GlobalValues.isSoundOn)
            {
                audioSource.PlayOneShot(clip);
            }
        }

        public static void PlayQbertDown()
        {
            instance.PlaySoundShot(instance.qbertDown);
        }

        public static void PlayCoinUp()
        {
            instance.PlaySoundShot(instance.coinUp);
        }

        public static void PlayWin()
        {
            instance.PlaySoundShot(instance.winLevel);
        }

        public static void PlayLevelTimer()
        {
            instance.PlaySoundShot(instance.levelTimer);
        }

        public static void PlayColoredCube()
        {
            instance.PlaySoundShot(instance.getColoredCube);
        }

        public static void PlayBlueCube()
        {
            instance.PlaySoundShot(instance.getBlueCube);
        }

        public static void PlayLevelStartLabel()
        {
            instance.PlaySoundShot(instance.levelStartLabel);
        }

        public static void PlayLevelLose()
        {
          //  instance.PlaySoundShot(instance.levelLose);
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
