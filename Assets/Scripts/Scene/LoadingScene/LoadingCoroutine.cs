using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace LD.Loading
{
    public class LoadingCoroutine : MonoBehaviour
    {
        Action m_LoadingEndCall;

        public void LoadScene(string sceneName, Action loadingEndCall)
        {
            m_LoadingEndCall = loadingEndCall;
            StartCoroutine(LoadScene(sceneName));
        }

        IEnumerator LoadScene(string sceneName)
        {
            // 切换到 loading 之后 0.1秒开始异步加载 场景
            yield return new WaitForSeconds(0.05f);
            Global.gApp.gGameCtrl.DestroyCurFrame();
            yield return new WaitForSeconds(0.05f);
            Debug.Log("Physics.defaultPhysicsScene " + Physics.defaultPhysicsScene.IsEmpty());

#if USE_ADDRESSABLES
            var handle = Addressables.LoadSceneAsync("Scene/" + sceneName + ".unity", LoadSceneMode.Single, true);
            handle.Completed += (AsyncOperationHandle<SceneInstance> obj) =>
            {
                if (obj.Status == AsyncOperationStatus.Succeeded)
                {
                    StartCoroutine(BTClient_LoadSceneSucceed());
                }
                else
                {
                    Debug.LogError("Failed to load scene at address: " + sceneName);
                }
            };
            while (!handle.IsDone)
            {
                Global.gApp.gMsgDispatcher.Broadcast(MsgIds.LoadingPercentValue, handle.PercentComplete);
                yield return 1;
            }
#else
            AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            loadSceneAsync.allowSceneActivation = false;
            while (!loadSceneAsync.isDone)
            {
                if (loadSceneAsync.progress >= 0.9f && !loadSceneAsync.allowSceneActivation)
                {
                    loadSceneAsync.allowSceneActivation = true;
                }
                yield return null;
            }
            // 场景 加载完毕 做一次 allowSceneActivation 保险
            if (!loadSceneAsync.allowSceneActivation)
            {
                yield return new WaitForSeconds(0.1f);
                loadSceneAsync.allowSceneActivation = true;
            }
            // 场景 加载完毕 做一次 调用 加载 结束的 回调
            yield return new WaitForSeconds(0.1f);
            if (m_LoadingEndCall != null)
            {
                m_LoadingEndCall();
            }
            Global.gApp.gResMgr.DestroyObj(this);
#endif
        }

        IEnumerator BTClient_LoadSceneSucceed()
        {
            // 场景 加载完毕 做一次 调用 加载 结束的 回调
            yield return new WaitForSeconds(0.05f);
            if (m_LoadingEndCall != null)
            {
                m_LoadingEndCall();
            }

            Global.gApp.gResMgr.DestroyBehaviour(this);
        }


        public void UnLoadLoadingScene(Action loadingEndCall)
        {
            m_LoadingEndCall = loadingEndCall;
            StartCoroutine(UnLoadScene());
        }

        IEnumerator UnLoadScene()
        {
            //0.1秒开始异步析构场景
            //yield return new WaitForSeconds(0.1f);

            //if (SceneManager.GetSceneByName("LoadingScene").isLoaded == true)
            //{
            //    AsyncOperation loadSceneAsync = SceneManager.UnloadSceneAsync("LoadingScene");
            //    while (!loadSceneAsync.isDone)
            //    {
            //        yield return null;
            //    }
            //}
            yield return new WaitForSeconds(0.05f);
            if (m_LoadingEndCall != null)
            {
                m_LoadingEndCall();
            }

            Global.gApp.gResMgr.DestroyBehaviour(this);
        }
    }
}