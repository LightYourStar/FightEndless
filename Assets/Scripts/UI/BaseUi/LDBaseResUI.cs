using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LD
{
    public class LDBaseResUI : MonoBehaviour
    {
        protected string m_Language = string.Empty;

        // 管理可以更精确点。比如 sprite 不用多次加载 价格cache 啥的。Addressables.LoadAssetAsync 会不会有内存泄漏 多次加载
        // 会多次 从内存里 在加载
        private Dictionary<string, Object> m_RefObj = new Dictionary<string, Object>();

        public void AdapterSmartUI(RectTransform rectTransform, float val)
        {
            if (rectTransform == null)
            {
                return;
            }
#if UNITY_IPHONE
            string modelStr = SystemInfo.deviceModel;
            foreach (string item in GameAdapterUtils.DynamicIslandDevices)
            {
                if(modelStr.Contains(item))
                {
                    rectTransform.offsetMax = rectTransform.offsetMax + new Vector2(0,val);
                }
            }
#endif
        }

        protected void PlayCommonWrongAudioClip()
        {
            // Global.gApp.gAudioSource.PlayOneShot(AudioConfig.Common_wrong, DYAudioType.DYUnPauseAudio);
        }

        protected void PlayCommonAudioClip()
        {
            // Global.gApp.gAudioSource.CommonClickAutio();
        }

        // protected void PlayUIAudioClip(string clip, DYAudioType audioType = DYAudioType.DYUnPauseAudio)
        // {
        //     if (string.IsNullOrEmpty(clip))
        //     {
        //         return;
        //     }
        //
        //     Global.gApp.gAudioSource.PlayOneShot(clip, audioType);
        // }

        public void LoadSprite(Image image, string path, bool mgr = false)
        {
            if (image == null)
            {
                return;
            }

            // if (string.IsNullOrEmpty(path))
            // {
            //     image.sprite = Extension.Enempty;
            //     return;
            // }

            if (m_RefObj.ContainsKey(path))
            {
                image.sprite = (Sprite)m_RefObj[path];
                return;
            }

            Sprite sprite = Global.gApp.gResMgr.LoadSprite(path);
            if (!mgr)
            {
                AddRef(path, sprite);
            }

            image.sprite = sprite;
        }
        // public void LoadSprite(RawImage image, string path, bool mgr = false)
        // {
        //     if (image == null)
        //     {
        //         return;
        //     }
        //     if (string.IsNullOrEmpty(path))
        //     {
        //         image.texture = null;
        //         return;
        //     }
        //     if (m_RefObj.ContainsKey(path))
        //     {
        //         image.texture = (Texture)m_RefObj[path];
        //         return;
        //     }
        //     Texture sprite = Global.gApp.gResMgr.LoadTexture(path);
        //     if (!mgr)
        //     {
        //         AddRef(path, sprite);
        //     }
        //     image.texture = sprite;
        // }

        // /// <summary>
        // /// 加载Sprite，注意：如果加载的Spritee资源之前加载过相同path的Texture则加载不出来
        // /// </summary>
        // /// <param name="path"></param>
        // /// <param name="mgr"></param>
        // /// <returns></returns>
        // public Sprite LoadSprite(string path, bool mgr = false)
        // {
        //     if (string.IsNullOrEmpty(path)) return Extension.Enempty;
        //     if (m_RefObj.ContainsKey(path))
        //     {
        //         return (Sprite)m_RefObj[path];
        //     }
        //     Sprite sprite = Global.gApp.gResMgr.LoadSprite(path);
        //     if (!mgr)
        //     {
        //         AddRef(path, sprite);
        //     }
        //     return sprite;
        //
        // }

        /// <summary>
        /// 加载Texture，注意：如果加载的Texture资源之前加载过相同path的Sprite则加载不出来
        /// </summary>
        /// <param name="path"></param>
        /// <param name="mgr"></param>
        /// <returns></returns>
        public Texture LoadTexture(string path, bool mgr = false)
        {
            if (string.IsNullOrEmpty(path)) return null;
            if (m_RefObj.ContainsKey(path))
            {
                return (Texture)m_RefObj[path];
            }

            Texture textTure = Global.gApp.gResMgr.LoadTexture(path);
            if (!mgr)
            {
                AddRef(path, textTure);
            }

            return textTure;
        }

        public void InstantiateObjAsync(string path, ResSceneType resSceneType, Transform parent, System.Action<GameObject> onLoadObj)
        {
            onLoadObj?.Invoke(InstantiateObj(path, resSceneType, parent));
        }

        public GameObject InstantiateObj(string path, ResSceneType resSceneType, Transform parent)
        {
            //if (string.IsNullOrEmpty(path)) return null;
            //GameObject prefab = null;
            //if (m_RefObj.ContainsKey(path))
            //{
            //    prefab = (GameObject)m_RefObj[path];
            //}
            //else
            //{
            //    prefab = Global.gApp.gResMgr.LoadAssets<GameObject>(path, ResType.Prefab);
            //    AddRef(path, prefab);
            //}
            //return Object.Instantiate<GameObject>(prefab, parent); ;
            return Global.gApp.gResMgr.InstantiateObj(path, ResSceneType.NormalUI, parent);
        }

        public GameObject InstanceUIEffectNode(string path, Transform parent, int AdapterOrder = -1, System.Action<GameObject> onComplete = null)
        {
            return InstantiateObj(path, ResSceneType.NormalUI, parent);
        }

        public GameObject InstanceUIEffectNode(string path, ResSceneType resType, Transform parent)
        {
            return InstantiateObj(path, resType, parent);
        }

        public Sprite LoadSpriteAtlas(string name, string spritePath)
        {
            if (string.IsNullOrEmpty(spritePath)) return null;
            string spriteAtlasName = name + spritePath;
            if (m_RefObj.ContainsKey(spriteAtlasName))
            {
                return (Sprite)m_RefObj[spriteAtlasName];
            }

            Sprite sprite = Global.gApp.gResMgr.LoadSprite(name, spritePath, false);
            AddRef(spriteAtlasName, sprite);
            return sprite;
        }

        protected void AddRef(string path, Object obj)
        {
            if (obj == null)
            {
                return;
            }

            m_RefObj.Add(path, obj);
        }

        protected void ReleaseRef()
        {
            ClearRef();
        }

        protected virtual void OnDestroy()
        {
            ClearRef();
        }

        private void ClearRef()
        {
#if USE_ADDRESSABLES
            if (m_RefObj == null)
            {
                return;
            }

            foreach (var item in m_RefObj)
            {
                Addressables.Release(item.Value);
            }

            m_RefObj.Clear();
#endif
        }

        public virtual void ItemInit()
        {
        }

        public virtual void ItemRecycle()
        {
        }

        /// <summary>
        ///  暂时支持 Text 后面可能需要支持 texInput
        /// </summary>
        protected void ChangeLanguage()
        {
            // string lgg = Global.gApp.gGameData.GetClientCurLanguage();
            // if (!lgg.Equals(m_Language))
            // {
            //     m_Language = lgg;
            //     ChangeLanguageImp();
            // }
        }

        protected void ForceChangeLanguage()
        {
            // string lgg = Global.gApp.gGameData.GetClientCurLanguage();
            // m_Language = lgg;
            ChangeLanguageImp();
        }

        protected void ChangeLanguageImp()
        {
            if (m_Language == "ar")
            {
                Extension.RTL = true;
            }
            else
            {
                Extension.RTL = false;
            }

            Text[] ts = gameObject.GetComponentsInChildren<Text>(true);
            foreach (Text t in ts)
            {
                t.raycastTarget = false;
//                 if (t.GetComponent<KeepFont>() == null)
//                 {
//                     // 暂时不改字体
//                     t.font = Global.gApp.gGameData.GetFont(m_Language);
//                 }
//                 if(m_Language == "zh_CN")
//                 {
//                     t.lineSpacing = Mathf.Max(1, t.lineSpacing);
//                 }
//
//                 LanguageTip lt = t.GetComponent<LanguageTip>();
//                 if (lt != null)
//                 {
//                     t.SetTips(lt.TipId);
// #if UNITY_EDITOR
//                     if (t.gameObject.activeInHierarchy && !Tips.Data.TryGet(lt.TipId, out _, false))
//                     {
//                         Debug.LogError($"LanguageTip Error : TipsId = {lt.TipId}", t.gameObject);
//                         t.text = "不存在的 tips ID" + lt.TipId;
//                     }
// #endif
//                 }
            }

            TextMeshProUGUI[] tsMeshPro = gameObject.GetComponentsInChildren<TextMeshProUGUI>(true);
            foreach (TextMeshProUGUI t in tsMeshPro)
            {
                t.raycastTarget = false;
//                 if (t.GetComponent<KeepFont>() == null)
//                 {
//                     Material material = t.fontSharedMaterial;
//                     t.font = Global.gApp.gGameData.GetTMPFont(m_Language);
//                     Global.gApp.gGameData.CalcTMPFont(t, m_Language, material);
//                 }
//
//                 LanguageTip lt = t.GetComponent<LanguageTip>();
//                 if (lt != null)
//                 {
//                     t.SetTips(lt.TipId);
// #if UNITY_EDITOR
//                     if (t.gameObject.activeInHierarchy && !Tips.Data.TryGet(lt.TipId, out _, false))
//                     {
//                         Debug.LogError($"LanguageTip Error : TipsId = {lt.TipId}", t.gameObject);
//                     }
// #endif
//                 }
            }
        }

        protected void AddEventTrigger(GameObject target, EventTriggerType eventType, UnityEngine.Events.UnityAction<BaseEventData> callback)
        {
            EventTrigger trigger = target.GetComponent<EventTrigger>() ?? target.AddComponent<EventTrigger>();
            var entry = new EventTrigger.Entry { eventID = eventType };
            entry.callback.AddListener(callback);
            trigger.triggers.Add(entry);
        }
    }
}