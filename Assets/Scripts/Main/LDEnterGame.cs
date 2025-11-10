using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace LD
{
    public class LDEnterGame
    {
        public static void StartGame(MonoBehaviour mono)
        {
            Debug.Log("EnterGame ");
            mono.StartCoroutine(LoadAOT());
        }

        private static IEnumerator LoadAOT()
        {
            yield return new WaitForSeconds(0.1f);

            //todo

            StartGameImp();

        }

        private static void StartGameImp()
        {
            InitAddressable();
            LoadGameDLL();
            LoadKeepNode();
        }

        private static void InitAddressable()
        {
#if USE_ADDRESSABLES
            AsyncOperationHandle<IResourceLocator> init = Addressables.InitializeAsync();
            init.WaitForCompletion();
            ReloadAddressable();
#endif
        }

        private static void ReloadAddressable()
        {
#if USE_ADDRESSABLES
            string catalogFile = MainIoUtils.BundlePath + "/catalog.json";
            Debug.Log(" InitAddressable 111 " + catalogFile);
            if (!File.Exists(catalogFile))
            {
                return;
            }
            Debug.Log(" InitAddressable 2222 " + catalogFile);
            Addressables.ClearResourceLocators();
            AsyncOperationHandle<IResourceLocator> loadLog = Addressables.LoadContentCatalogAsync(catalogFile);
            loadLog.WaitForCompletion();
#endif
        }

        public static void LoadGameDLL()
        {
            Debug.Log(" MainUpdate Main LoadGameDLL LoadGameDLL  ");
            if (false && RuntimeSettings.HybridCLREnable)
            {
                string bundlePath = MainIoUtils.GameDLLBundlePath;
                Debug.Log(bundlePath);

                Debug.Log(" MainUpdate Main Load sdcard bundlePath ");
                AsyncOperationHandle<TextAsset> operationHandle = Addressables.LoadAssetAsync<TextAsset>(bundlePath);
                TextAsset assetBundle = operationHandle.WaitForCompletion();
                System.Reflection.Assembly.Load(assetBundle.bytes);
                Debug.Log(" MainUpdate Main Load Game Ended");

                //if (System.IO.File.Exists(bundlePath))
                //{

                //    byte[] assemblyData = System.IO.File.ReadAllBytes(bundlePath);

                //}
            }
        }

        public static void LoadKeepNode()
        {
            ReloadAddressable();
            AsyncOperationHandle<GameObject> keepNodeHandle = Addressables.InstantiateAsync("PrefabsN/KeepNode.prefab");
            keepNodeHandle.WaitForCompletion();
        }
    }
}