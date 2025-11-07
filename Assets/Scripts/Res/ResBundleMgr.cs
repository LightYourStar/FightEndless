using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace LD
{
    public class ResBundleMgr : ResMgr
    {
        public ResBundleMgr()
        {

        }
        //public override void UnLoadAssets()
        //{
        //    foreach (KeyValuePair < ResSceneType, Dictionary<string, GameObject>> kv in m_CachePrefabs)
        //    {
        //        UnloadRes(kv.Value);
        //    }
        //    base.UnLoadAssets();
        //}

        public override U LoadAssets<U>(string path, ResType resType)
        {
            try
            {
                if (path == string.Empty)
                {
                    Debug.LogError(" 加载资源失败1 ，资源路径名称 没配置 " + path);
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#endif
                    return null;
                }

#if UNITY_EDITOR

                // if (!ExcelDataCheck.FileExitByLoad(string.Format(m_ResTypeSuffix[resType], path)))
                // {
                //     Debug.LogError(" 加载资源失败，请检查资源路径、大小写、名字是否正确 " + string.Format(m_ResTypeSuffix[resType], path));
                //     UnityEditor.EditorApplication.isPlaying = false;
                //     return null;
                // }
#endif

                AsyncOperationHandle<U> operationHandle = Addressables.LoadAssetAsync<U>(string.Format(m_ResTypeSuffix[resType], path));
                if (operationHandle.Status == AsyncOperationStatus.Failed)
                {
                    Debug.LogError(" 加载资源失败，请导出资源 " + path);
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#endif
                    return null;
                }
                return operationHandle.WaitForCompletion();
            }
            catch (System.Exception e)
            {
                Debug.LogError("exception " + e.Message);
                Debug.LogError(" 加载资源异常 " + path);
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                return null;
            }


        }
    }
}