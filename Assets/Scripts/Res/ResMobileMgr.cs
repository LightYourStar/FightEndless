using UnityEngine;
using UnityEngine.Networking;

namespace LD
{
    public class ResMobileMgr : ResMgr
    {
        AssetBundle m_AssetBundle = null;
        public static string GetTextForStreamingAssets()
        {
            return ""; //#todo
            // var path = $"{LD.MainUtils.StreamAssetsPath}/bundle/{"res"}";
            // return path;
        }
        public ResMobileMgr()
        {

        }
        System.Action m_PreLoadCallBack;
        public override void PreLoadAssets(System.Action action)
        {
            action.Invoke();
            //return;
            //m_PreLoadCallBack = action;
            //Global.gApp.gGlobal.StartCoroutine(LoadAllRes());
        }
        System.Collections.IEnumerator LoadAllRes()
        {
            yield return new WaitForSeconds(0.1f);

            string bundlePath = Application.persistentDataPath + "/bundle/res";
            if (System.IO.File.Exists(bundlePath))
            {
                byte[] abBytes = System.IO.File.ReadAllBytes(bundlePath);
                m_AssetBundle = AssetBundle.LoadFromMemory(abBytes);
                m_PreLoadCallBack?.Invoke();
                m_PreLoadCallBack = null;
            }
            else
            {
                string KeepNodePath = GetTextForStreamingAssets();
                using (UnityWebRequest www = UnityWebRequest.Get(KeepNodePath))
                {
                    yield return www.SendWebRequest();

                    if (www.result != UnityWebRequest.Result.Success)
                    {
                        Debug.LogError(www.error);
                        PreLoadAssets(m_PreLoadCallBack);
                    }
                    else
                    {
                        byte[] abBytes = www.downloadHandler.data;
                        m_AssetBundle = AssetBundle.LoadFromMemory(abBytes);
                        m_PreLoadCallBack?.Invoke();
                        m_PreLoadCallBack = null;
                    }
                }
            }
        }
        public override U LoadAssets<U>(string path, ResType resType)
        {
            string fileName = System.IO.Path.GetFileName(path);
            return m_AssetBundle.LoadAsset<U>(fileName);
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            if (m_AssetBundle != null)
            {
                m_AssetBundle.Unload(true);
                m_AssetBundle = null;
            }
        }
    }
}