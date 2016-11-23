using System.Collections;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene
{
    public class FlashSprite : MonoBehaviour
    {
        public SpriteRenderer spriteFlash;
        private float timeBetweenFlares = 0.5f;
        private float currentFlashTime;
        private bool isFlash;

        public void StartFlash(float time)
        {
            currentFlashTime = time;
            if (!isFlash)
            {
                isFlash = true;
                StartCoroutine(IEFlash());
            }
        }

        IEnumerator IEFlash()
        {
            yield return null;

            spriteFlash.gameObject.SetActive(true);

            while (currentFlashTime > 0)
            {
                currentFlashTime -= Time.deltaTime;
                yield return null;
            }

            spriteFlash.gameObject.SetActive(false);
            isFlash = false;
        }

        public void StopFlash()
        {
            StopAllCoroutines();
            spriteFlash.gameObject.SetActive(false);
            isFlash = false;
        }
    }
}