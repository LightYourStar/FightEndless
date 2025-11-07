using System;
using System.Diagnostics;
using UnityEngine;
using Google.Protobuf;
using Debug = UnityEngine.Debug;

namespace LD
{
    public class Global : MonoBehaviour
    {
        public static App gApp = new App();
        public static bool ShowLog = true;

        void Awake()
        {
            DontDestroyOnLoad(gameObject.transform.parent.gameObject);

            Extension.Extend();
            UnityEngine.Random.InitState(DateTime.Now.Second);
            gApp.Awake(this, gameObject.transform.parent.gameObject);
            // gameObject.AddComponent<LDCheckUpDataUtil>().StartCheckUpdate();
            // 内存不足的回调 全部预留
            Application.lowMemory -= OnLowMemory;
            Application.lowMemory += OnLowMemory;
        }

        private void Update()
        {
            float dtTime = Time.deltaTime;
            if (gApp != null)
            {
                // gApp.DUpdate(dtTime);
            }
        }

        private void OnDestroy()
        {
#if (UNITY_EDITOR)
            if (gApp != null)
            {
                gApp.OnDestroy();
            }

            Resources.UnloadUnusedAssets();
            System.GC.Collect();
#endif
        }

        public static void Log(object logStr)
        {
            Debug.Log(logStr);
        }

        [Conditional("DEBUG")]
        public static void LogEditor(object logStr)
        {
            Debug.Log(logStr);
        }

        [Conditional("DEBUG")]
        public static void LogErrorEditor(object logStr)
        {
            Debug.LogError(logStr);
        }

        public static void LogError(object logStr)
        {
            Debug.LogError(logStr);
        }

        public static void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public static int GetGameVersion()
        {
            return 1;
        }

        public static void LogGreen(string msg)
        {
            msg = string.Format("<color=green>{0}</color>", msg);
            Debug.Log(msg);
        }

        /// <summary>
        /// 发协议专用  方便Debug
        /// </summary>
        /// <param name="msg"></param>
        public static void LogRequest(string msg)
        {
            msg = string.Format("<color=orange>[Client]Request: {0}</color>", msg);
            Debug.Log(msg);
        }

        /// <summary>
        /// 收协议专用  方便Debug
        /// </summary>
        /// <param name="msg"></param>
        public static void LogRespond(string msg)
        {
            msg = string.Format("<color=yellow>[Server]Respond: {0}</color>", msg);
            Debug.Log(msg);
        }

        private void OnApplicationFocus(bool focus)
        {
            // gApp?.OnApplicationFocus(focus);
        }

        public static void OnLowMemory()
        {
            // gApp?.OnLowMemory();
        }

        public static int GetTouchCount()
        {
#if ((UNITY_ANDROID || UNITY_IOS || UNITY_WINRT || UNITY_BLACKBERRY) && !UNITY_EDITOR)
		return Input.touchCount;
#else
            if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
            {
                return 1;
            }
            else
            {
                return 0;
            }
#endif
        }
    }
}