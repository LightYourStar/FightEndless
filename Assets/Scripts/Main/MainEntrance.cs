using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD
{
    public class MainEntrance : MonoBehaviour
    {
        private void Awake()
        {
            InitFocusInfo();
            AdapterCanvas();


        }

        /// <summary>
        /// 初始化焦点信息
        /// </summary>
        private void InitFocusInfo()
        {
#if UNITY_EDITOR
            Application.runInBackground = true;
#else
            Application.runInBackground = false;
#endif
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        private void AdapterCanvas()
        {
            LDUIAdapter.AdapterMainScene(gameObject.transform);
        }

        private void Start()
        {
            StartGame();
        }

        private void StartGame()
        {
            LDEnterGame.StartGame(this);
        }

        /// <summary>
        /// ui 适配留黑边 需要 clear color
        /// </summary>
        private void Update()
        {
#if !UNITY_IPHONE
            GL.Clear(false, true, Color.black);
#endif
        }
    }
}