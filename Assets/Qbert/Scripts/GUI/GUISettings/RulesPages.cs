using UnityEngine;

namespace Assets.Qbert.Scripts.GUI.GUISettings
{
    public class RulesPages : MonoBehaviour
    {
        public int currentPage = 0;

        public Transform[] pages;
        public TextMesh currentPageText;


        public void NextPage()
        {
            if (currentPage < pages.Length - 1)
            {
                HideAllPages();

                currentPage++;
                pages[currentPage].gameObject.SetActive(true);

                UpdateTextCurrentPage();
            }
        }
        public void PrivPage()
        {
            if (currentPage > 0)
            {
                HideAllPages();

                currentPage--;
                pages[currentPage].gameObject.SetActive(true);

                UpdateTextCurrentPage();
            }
        }

        public void UpdateTextCurrentPage()
        {
            currentPageText.text = string.Format("{0}/{1}", currentPage + 1, pages.Length);
        }

        private void HideAllPages()
        {
            foreach (var page in pages)
            {
                page.gameObject.SetActive(false);
            }
        }

        void Start ()
        {
            UpdateTextCurrentPage();

        }
	
        void Update () 
        {
	
        }
    }
}
