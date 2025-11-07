using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LD
{
    public partial class GameCtrl
    {
        private FrameCtrl m_FrameCtrl = null;
        // private LoadingFrameCtrl m_LoadingFrameCtrl = null;

        public float FrameDtTime { get; private set; }

        public void DUpdate(float dt)
        {
            if (m_FrameCtrl != null)
            {
                m_FrameCtrl.Update(dt);
            }
        }

        IEnumerator LoadSceneAsync(string sceneName, int loadingType, Action cb)
        {
            yield return new WaitForSeconds(0.05f);

#if USE_ADDRESSABLES
            AsyncOperationHandle sceneHandle = Addressables.LoadSceneAsync("Scene/LoadingScene.unity");
            sceneHandle.Completed += (AsyncOperationHandle obj) => { Global.gApp.gGlobal.StartCoroutine(OnLoadLoadingScene(sceneName, loadingType, cb)); };
#else
            SceneManager.LoadScene("LoadingScene");
#endif
        }

        #region ChangeToLoginScene

        public void ChangeToLoginScene()
        {
            // ClearGlobalTouchMask();
            // SetTargetFrame(LDPlatformCfg.TargetFrameRate);
            // 清除所有的消息。包括 MarkAsPermanent 的
            //Global.gApp.gMsgDispatcher.ForceCleanup();
            //Global.gApp.gUiMgr.CloseAllUI();
            Global.gApp.gUiMgr.ClossAllNormalUI();

            //#todo
            // Global.gApp.gUiMgr.OpenUIAsync<LoadingUI>(LDUICfg.LoadingUI).SetLoadedCall(ui =>
            // {
            //     ui?.RefreshUI(0, () => { Global.gApp.gGlobal.StartCoroutine(LoadSceneAsync("LoginScene", 0, ChangeToLoginSceneFormLoading)); });
            // });
// #if USE_ADDRESSABLES
//             AsyncOperationHandle sceneHandle = Addressables.LoadSceneAsync("Scene/LoadingScene.unity");
//             sceneHandle.Completed += (AsyncOperationHandle obj) =>
//             {
//                 Global.gApp.gGlobal.StartCoroutine(OnLoadLoadingScene("LoginScene", 0, ChangeToLoginSceneFormLoading));
//             };
//             return;
// #else
//             SceneManager.LoadScene("LoadingScene");
// #endif
        }

        public void ChangeToLoginSceneFormLoading()
        {
            Global.gApp.gUiMgr.CloseAllUI();
            // LoginScene scene = new LoginScene();
            // FrameCtrl frameCtrl = new LoginFrameCtrl(scene);
            // ChangeCtrl(frameCtrl);
            // scene.CacheAndOpenUI();
            System.GC.Collect();
        }

        #endregion

        #region ChangeToMainScene

        public void TryChangeToMainScene()
        {
            ChangeToMainScene();
        }

        public void ChangeToMainScene()
        {
            Debug.Log("Start ChangeTo MainScene");
            // ClearGlobalTouchMask();
            SetTargetFrame(60);
            // SetTargetFrame(LDPlatformCfg.TargetFrameRate);
            //Global.gApp.gMsgDispatcher.Cleanup();
            Global.gApp.gUiMgr.ClossAllNormalUI();
            // if (m_LoadingFrameCtrl != null)
            // {
            //     m_LoadingFrameCtrl.IsLoadingScene = true;
            // }

            //#todo
            // Global.gApp.gUiMgr.OpenUIAsync<LoadingUI>(LDUICfg.LoadingUI).SetLoadedCall(ui =>
            // {
            //     ui?.RefreshUI(0, () => { Global.gApp.gGlobal.StartCoroutine(LoadSceneAsync("MainScene", 0, ChangeToMainSceneFormLoading)); });
            // });

// #if USE_ADDRESSABLES
//             AsyncOperationHandle sceneHandle = Addressables.LoadSceneAsync("Scene/LoadingScene.unity");
//             sceneHandle.Completed += (AsyncOperationHandle obj) =>
//             {
//                 Global.gApp.gGlobal.StartCoroutine(OnLoadLoadingScene("MainScene", 0, ChangeToMainSceneFormLoading));
//
//             };
// #else
//             SceneManager.LoadScene("LoadingScene");
// #endif
        }

        public void ChangeToMainSceneFormLoading()
        {
            CampsiteScene scene = new CampsiteScene();
            FrameCtrl frameCtrl = new CampsiteFrameCtrl(scene);
            ChangeCtrl(frameCtrl);
            System.GC.Collect();
        }

        private void ChangeCtrl(FrameCtrl frameCtrl)
        {
            m_FrameCtrl = frameCtrl;
            m_FrameCtrl.Init();
            // if (m_LoadingFrameCtrl != null) m_LoadingFrameCtrl.IsLoadingScene = false;
        }

        //设置游戏帧率
        public void SetTargetFrame(int targetFrame)
        {
            Application.targetFrameRate = targetFrame;
            FrameDtTime = 1.0f / targetFrame;
        }

        #endregion
    }
}