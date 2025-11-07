using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D;

namespace LD
{
    public enum ResType
    {
        Prefab = 0,//预制
        SpriteAtlas,//图集
        Sprite,//图片
        Clip,//音效
        Font,//字体
        Asset,//unity资源
        Material,//unity资源
        Texture,//图片
    }
    public enum ResSceneType
    {
        // NormalRes = 1,// 特效 + 角色 加武器啥的 + ui 战斗特效 灯
        FightUI = 2, // 战斗ui

        NormalRes = 11,//
        NormalUI = 12,// 普通ui

        //SelfClose = 20, // 只能自己关的ui

        Resident = 101,// 常驻资源。不卸载 且 只能自己关的ui
        //ResidentNormal = 102,// 常驻资源。不卸载 可以一起关 的ui

    }
    public class ResMgr
    {

        // 先对 prefab 进行拆分

        protected Dictionary<ResType, string> m_ResTypeSuffix;
        protected Dictionary<ResSceneType, Dictionary<string, GameObject>> m_CachePrefabs;
        protected Dictionary<string, SpriteAtlas> m_SpriteAtlas;
        protected Dictionary<string, AudioClip> m_AllClip;
        protected Dictionary<string, Font> m_AllFont;
        protected Dictionary<string, TMP_FontAsset> m_AllTMPFont;
        protected Dictionary<string, Material> m_AllTMPMaterial;
        public ResMgr()
        {
            m_CachePrefabs = new Dictionary<ResSceneType, Dictionary<string, GameObject>>();
            m_SpriteAtlas = new Dictionary<string, SpriteAtlas>();
            m_AllClip = new Dictionary<string, AudioClip>();
            m_AllFont = new Dictionary<string, Font>();
            m_AllTMPFont = new Dictionary<string, TMP_FontAsset>();
            m_AllTMPMaterial = new Dictionary<string, Material>();
            m_ResTypeSuffix = new Dictionary<ResType, string>();

            foreach (ResSceneType day in System.Enum.GetValues(typeof(ResSceneType)))
            {
                m_CachePrefabs.Add(day, new Dictionary<string, GameObject>());
            }
            m_ResTypeSuffix.Add(ResType.Prefab, "{0}.prefab");
            m_ResTypeSuffix.Add(ResType.SpriteAtlas, "{0}.spriteAtla");
            m_ResTypeSuffix.Add(ResType.Sprite, "{0}.png");
            m_ResTypeSuffix.Add(ResType.Clip, "{0}.mp3");
            m_ResTypeSuffix.Add(ResType.Font, "{0}.ttf");
            m_ResTypeSuffix.Add(ResType.Asset, "{0}.asset");
            m_ResTypeSuffix.Add(ResType.Material, "{0}.mat");

        }
        public virtual void PreLoadAssets(System.Action action)
        {
            action();
        }
        public GameObject PreLoadPrefab(string path, ResSceneType resSceneType)
        {
            return LoadPrefab(path, resSceneType);
        }
        private GameObject LoadPrefab(string path, ResSceneType resSceneType)
        {
            Dictionary<string, GameObject> prefabs = m_CachePrefabs[resSceneType];
            if (prefabs.ContainsKey(path))
            {
                GameObject prefab = prefabs[path];
                return prefab;
            }
            else
            {
                GameObject prefab = LoadAssets<GameObject>(path, ResType.Prefab);
                prefabs.Add(path, prefab);
                return prefab;
            }
        }
        public virtual void UnLoadAssets()
        {
            // 清空的时候注意释放引用
            Resources.UnloadUnusedAssets();
            System.GC.Collect();
        }
        public void TryReleaseUI(string resPath)
        {
            Dictionary<string, GameObject> resUIs = m_CachePrefabs[ResSceneType.NormalUI];
            if(resUIs.ContainsKey(resPath))
            {
                Addressables.Release(resUIs[resPath]);
                resUIs.Remove(resPath);
            }
        }
        public void UnLoadCrossSceneAssets()
        {
            UnloadRes(m_CachePrefabs[ResSceneType.NormalRes]);
            UnloadRes(m_CachePrefabs[ResSceneType.NormalUI]);
            UnloadRes(m_CachePrefabs[ResSceneType.FightUI]);
            this.UnLoadAssets();
        }
        protected void UnloadRes(Dictionary<string, GameObject> keyValuePairs)
        {
            foreach (KeyValuePair<string, GameObject> item in keyValuePairs)
            {
                Addressables.Release(item.Value);
            }
            keyValuePairs.Clear();
        }

        public void UnloadRes(ResSceneType resSceneType,string path)
        {
            if(m_CachePrefabs.ContainsKey(resSceneType) && m_CachePrefabs[resSceneType].ContainsKey(path))
            {
                Addressables.Release(m_CachePrefabs[resSceneType][path]);
                m_CachePrefabs[resSceneType].Remove(path);
            }
        }
    /// <summary>
    /// 底层方法禁止其他人使用
    /// </summary>
    /// <param name="path"></param>
    /// <param name="resSceneType"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
        public GameObject InstantiateObj(string path,ResSceneType resSceneType, Transform parent = null)
        {
            GameObject obj = LoadPrefab(path, resSceneType);
            if (obj == null)
            {
                Debug.LogError("obj is null, the path : " + path);
                return new GameObject();
            }
            return Object.Instantiate<GameObject>(obj, parent);
        }
        public GameObject InstantiateLoadObj(string path, ResSceneType resSceneType, Transform parent = null)
        {
            return InstantiateObj(path, resSceneType, parent);
        }
        public GameObject InstanceDiyPart(string path, ResSceneType resSceneType, Transform parent = null)
        {
            return InstantiateLoadObj(path, resSceneType, parent);
        }
        public GameObject InstantiateEffectObj(string path, ResSceneType resSceneType, Transform parent = null)
        {
            return InstantiateObj(path, resSceneType, parent);
        }

        public AudioClip LoadAudioClip(string clipName, bool mgr = true)
        {
            AudioClip audioClip;
            if (string.IsNullOrEmpty(clipName))
            {
                return null;
            }
            if (!m_AllClip.TryGetValue(clipName, out audioClip))
            {
                audioClip = LoadAssets<AudioClip>("Sound/" + clipName, ResType.Clip);
                if (mgr)
                {
                    m_AllClip.Add(clipName, audioClip);
                }
            }

            if (audioClip == null)
            {
                Debug.LogWarning("AudioClip not found  " + clipName);
            }
            return audioClip;
        }

        public Sprite LoadSprite(string spritePath)
        {
            return LoadAssets<Sprite>(spritePath, ResType.Sprite);
        }

        public Texture LoadTexture(string texturePath)
        {
            return LoadAssets<Texture>(texturePath, ResType.Sprite);
        }

        public Sprite LoadSprite(string name, string spritePath, bool mgr)
        {
            SpriteAtlas atlas = LoadSpriteAtlas(ref spritePath, mgr);
            return atlas.GetSprite(name);
        }
        public Font LoadFont(string path)
        {
            Font font;
            if (!m_AllFont.TryGetValue(path, out font))
            {
                font = LoadAssets<Font>(path, ResType.Font);
                m_AllFont[path] = font;
            }
            return font;
        }
        public TMP_FontAsset LoadTMPFont(string path)
        {
            TMP_FontAsset font;
            if (!m_AllTMPFont.TryGetValue(path, out font))
            {
                font = LoadAssets<TMP_FontAsset>(path, ResType.Asset);
                m_AllTMPFont[path] = font;
            }
            return font;
        }
        public Material LoadTMPMaterial(string path)
        {
            Material mat;
            if (!m_AllTMPMaterial.TryGetValue(path, out mat))
            {
                mat = LoadAssets<Material>(path, ResType.Material);
                m_AllTMPMaterial[path] = mat;
            }
            return mat;
        }

        public SpriteAtlas LoadSpriteAtlas(ref string path, bool mgr)
        {
            if (mgr)
            {
                if (m_SpriteAtlas.ContainsKey(path))
                {
                    SpriteAtlas spriteAtlas = m_SpriteAtlas[path];
                    return spriteAtlas;
                }
                else
                {
                    SpriteAtlas spriteAtlas = LoadAssets<SpriteAtlas>(path, ResType.SpriteAtlas);
                    m_SpriteAtlas[path] = spriteAtlas;
                    return spriteAtlas;
                }
            }
            else
            {
                return LoadAssets<SpriteAtlas>(path, ResType.SpriteAtlas);
            }

        }
        public virtual U LoadAssets<U>(string path, ResType resType) where U : UnityEngine.Object
        {
            return Resources.Load<U>(path);
        }

        public T LoadGameDataN<T>(string name) where T : ScriptableObject
        {
            string path = "Config/" + name;
            return LoadAssets<T>(path, ResType.Asset);
        }

        public ScriptableObject LoadGameData(string name)
        {
            string path = "Config/" + name;
            return LoadAssets<ScriptableObject>(path, ResType.Asset);
        }
        public void DestroyGameObj(GameObject go)
        {
            if (go != null)
            {
                go.SetActive(false);
                GameObject.Destroy(go);
            }
        }
        public void DestroyBehaviour(Behaviour go)
        {
            if (go != null)
            {
                go.enabled = false;
                GameObject.Destroy(go);
            }
        }
        public virtual void OnDestroy()
        {
            m_AllFont.Clear();
            UnLoadAssets();
        }
    }
}