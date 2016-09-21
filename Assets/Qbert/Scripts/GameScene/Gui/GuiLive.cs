using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.GameScene.Gui
{
    public class GuiLive : MonoBehaviour
    {
        public int ifOverMaxShowNumber = 4;

        public Transform root;
        public Transform exampleLive;

        public Transform[] lives;

        public Transform rootIfOverMax;
        public Text textCountLives;

        public void SetLiveCount(int count)
        {
            UnityEngine.Debug.Log(count);
            lives = root.Cast<Transform>().ToArray();

            if (count > lives.Length)
            {
                for (int i = 0; i <  count - lives.Length; i++)
                {
                    AddLive();
                }
            }
            else if (count < lives.Length)
            {
                for (int i = 0; i < lives.Length - count; i++)
                {
                    RemoveLive();
                }
            }
            ShowHumberIfnead();
        }


        public void AddLive()
        {
            Transform newLive = Instantiate(exampleLive);
            newLive.gameObject.SetActive(true);

            var scale = newLive.localScale;
            newLive.SetParent(root);
            newLive.localScale = scale;
            newLive.localPosition = Vector3.zero;

            ShowHumberIfnead();
        }

        public void RemoveLive()
        {
            lives = root.Cast<Transform>().ToArray();
            if (lives.Length > 0)
            {
                var live = lives.First();
                live.gameObject.SetActive(false);
                live.SetParent(null);
                Destroy(live.gameObject);
            }

            ShowHumberIfnead();
        }


        public void ShowHumberIfnead()
        {
            lives = root.Cast<Transform>().ToArray();

            if (lives.Length > ifOverMaxShowNumber)
            {
                root.gameObject.SetActive(false);
                rootIfOverMax.gameObject.SetActive(true);
            }
            else
            {
                root.gameObject.SetActive(true);
                rootIfOverMax.gameObject.SetActive(false);
            }


            textCountLives.text = string.Format("- {0}", lives.Length);
        }


        void Start ()
        {

            // SetLiveCount(0);
            // StartCoroutine(AddTest());
        }


        IEnumerator AddTest()
        {
            RemoveLive();
            RemoveLive();
            yield return new WaitForSeconds(1.0f);

            for (int i = 0; i < 7; i++)
            {
                AddLive();
                yield return new WaitForSeconds(1.5f);
            }


            for (int i = 0; i < 7; i++)
            {
                RemoveLive();
                yield return new WaitForSeconds(1.5f);
            }

        }
	
        void Update () 
        {
	
        }
    }
}
