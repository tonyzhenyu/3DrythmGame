using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace ZYTools
{
    public class PrintTextAnimation : MonoBehaviour
    {
        #region Properties
        public string str;
        public float duration = 0.02f;
        public bool isSkip = false;
        #endregion

        Text text;
        int currentIndex = 0;

        #region UnityMessage
        private void Awake()
        {
            try
            {
                text = GetComponent<Text>();
            }
            catch (System.Exception)
            {
                Debug.LogError("NoTextComponent");
            }
        }
        void Start()
        {
            text.text = "";
        }
        private void OnEnable()
        {
            StartCoroutine(PrintText());
        }
        private void OnDestroy()
        {
            StopAllCoroutines();
        }
        #endregion

        IEnumerator PrintText()
        {
            while (true)
            {
                //delay
                yield return new WaitForSeconds(duration);

                if (currentIndex >= str.Length - 1 || isSkip == true)
                {
                    text.text = str;
                    Destroy(this);
                }
                currentIndex += 1;
                text.text = str.Substring(0, Mathf.Min(currentIndex, str.Length));

            }
        }
    }

}
