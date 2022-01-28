using UnityEngine;

namespace ZYTools
{
    public class FpsDebugUI : MonoBehaviour
    {
        enum RestPort { UpLeft, UpRight, DownLeft, DownRight }

        #region Properties
        public float    updateStep  = 0.02f;
        private float   deltaTime   = 0;
        private int count           = 0;
        private string  strFpsInfo;
        [SerializeField] private RestPort restPort;
        [SerializeField] private GUIStyle gUIStyle;
        #endregion

        void Update()
        {
            count++;
            deltaTime += Time.deltaTime;

            if (deltaTime >= updateStep)
            {
                float fps           = count / deltaTime;
                float milliSecond   = deltaTime * 1000 / count;

                strFpsInfo = $" {milliSecond:0.0} ms \n Fps:{fps:0.}";

                count       = 0;
                deltaTime   = 0f;
            }
        }
        
        private void OnGUI()
        {
            switch (restPort)
            {
                case RestPort.UpLeft:
                    GUI.Label(new Rect(30, 10, 100, 200), strFpsInfo, gUIStyle);
                    break;
                case RestPort.UpRight:
                    GUI.Label(new Rect(Screen.width - 100, 10, 100, 200), strFpsInfo, gUIStyle);
                    break;
                case RestPort.DownLeft:
                    GUI.Label(new Rect(30, Screen.height - 100, 100, 200), strFpsInfo, gUIStyle);
                    break;
                case RestPort.DownRight:
                    GUI.Label(new Rect(Screen.width - 100, Screen.height - 100, 100, 200), strFpsInfo, gUIStyle);
                    break;
                default:
                    break;
            }
        }
    }
}