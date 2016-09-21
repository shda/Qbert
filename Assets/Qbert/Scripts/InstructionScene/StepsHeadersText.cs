using UnityEngine;
using System.Collections;
using Scripts.Utils;
using UnityEngine.UI;

namespace Scripts.InstructionScene
{
    public class StepsHeadersText : MonoBehaviour
    {
        public Text[] stepsHeadeLabels;

        public float durationColorChange = 0.3f;

        public void SetStepHeader(int step)
        {
            StartCoroutine(SetNextHeader(step));
        }


        IEnumerator SetNextHeader(int step)
        {
            Text oldText = null;

            foreach (var stepsHeadeLabel in stepsHeadeLabels)
            {
                if (stepsHeadeLabel.gameObject.activeSelf && oldText == null)
                {
                    oldText = stepsHeadeLabel;
                    continue;
                }
                stepsHeadeLabel.gameObject.SetActive(false);
            }

            if (oldText != null)
            {
                oldText.color = new Color(1, 1, 1, 1);
                yield return StartCoroutine(this.ChangeColor(new Color(1, 1, 1, 1), color =>
                {
                    oldText.color = color; 
                }, new Color(1, 1, 1, 0) , durationColorChange));

                oldText.gameObject.SetActive(false);
            }

            var textShow = stepsHeadeLabels[step - 1];

            textShow.color = new Color(1, 1, 1, 0);
            textShow.gameObject.SetActive(true);
            yield return StartCoroutine(this.ChangeColor(new Color(1, 1, 1, 0), color =>
            {
                textShow.color = color;
            }, new Color(1, 1, 1, 1), durationColorChange));

        }

        public void HideAllHeader()
        {
            foreach (var stepsHeadeLabel in stepsHeadeLabels)
            {
                stepsHeadeLabel.gameObject.SetActive(false);
            }
        }

        void Start()
        {

        }

        void Update()
        {

        }
    }
}

