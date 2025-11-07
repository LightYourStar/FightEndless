using System.Collections.Generic;
using Cheetah.Common.SerializeTools;
using LD;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(LDBaseResUI), true)]
public class DYBaseUIEditor : Editor
{
    private LDBaseResUI _ui;

    private void OnEnable()
    {
        _ui = target as LDBaseResUI;

    }

    public override void OnInspectorGUI()
    {
        if (GameEditorTools.DrawHeader("BaseInspector", "BaseInspector_key"))
        {
            base.OnInspectorGUI();
        }
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Serialized"))
        {
            SerializedComponentWindow.OpenSerializedComponentWindow();
        }
        if (GUILayout.Button("FindMissFont"))
        {
            List<Text> textList = new List<Text>();
            (target as MonoBehaviour)?.transform.GetComponentsInChildren(true,textList);
            foreach (var text in textList)
            {
                if (text.font == null)
                {
                    Debug.LogError(text.name);
                }
            }
            Debug.Log("find end");
        }

        #region 定位到变量代码

        if (GUILayout.Button("查看变量代码"))
        {
            var monoScript = MonoScript.FromMonoBehaviour(_ui);
            string scriptFile = AssetDatabase.GetAssetPath(monoScript);
            InternalEditorUtility.OpenFileAtLineExternal(scriptFile, 0);
        }

        #endregion

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        #region 定位到逻辑代码

        if (GUILayout.Button("查看逻辑代码"))
        {
            string className = _ui.GetType().Name;

            // 搜索项目中所有 .cs 文件
            string[] guids = AssetDatabase.FindAssets($"{className} t:MonoScript");
            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                string[] names = path.Split("/");
                if (names[^1].Equals($"{className}_Logic.cs"))
                {
                    InternalEditorUtility.OpenFileAtLineExternal(path, 0);
                    return;
                }
            }
        }

        #endregion

        #region 在Project中定位Prefab

        if (GUILayout.Button("定位Prefab"))
        {
            GameObject go = _ui.gameObject;
            Object prefabAsset = null;

            //Prefab模式
            PrefabStage stage = PrefabStageUtility.GetPrefabStage(go);
            if (stage != null && !string.IsNullOrEmpty(stage.assetPath))
            {
                prefabAsset = AssetDatabase.LoadAssetAtPath<Object>(stage.assetPath);
            }

            //场景实例
            if (prefabAsset == null)
            {
                prefabAsset = PrefabUtility.GetCorrespondingObjectFromSource(go);
            }

            //运行时，名字含(Clone)
            if (prefabAsset == null && go.name.EndsWith("(Clone)"))
            {
                string pureName = go.name.Replace("(Clone)", "").Trim();
                string[] guids = AssetDatabase.FindAssets(pureName + " t:prefab");
                if (guids.Length > 0)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                    prefabAsset = AssetDatabase.LoadAssetAtPath<Object>(path);
                }
            }

            // 当前对象就是Prefab资源
            if (prefabAsset == null)
            {
                string path = AssetDatabase.GetAssetPath(go);
                if (!string.IsNullOrEmpty(path) && path.EndsWith(".prefab"))
                {
                    prefabAsset = go;
                }
            }

            if (prefabAsset != null)
            {
                Selection.activeObject = prefabAsset;
                EditorGUIUtility.PingObject(prefabAsset);
            }
        }

        #endregion

        EditorGUILayout.EndHorizontal();
    }
}