using UnityEngine;

namespace LD
{
    public class MainIoUtils
    {
        public static string BundlePath;
        public static string GameDLLBundlePath;
        public static string GameDllName = "logo_36_ct.bytes";

        static MainIoUtils()
        {
            RuntimePlatform platform = Application.platform;
            if (platform == RuntimePlatform.WindowsEditor || platform == RuntimePlatform.OSXEditor)
            {
                BundlePath = Application.dataPath + "/../StreamingAssets/bundle/";
            }
            else
            {
                BundlePath = Application.persistentDataPath + "/bundle/";
            }

            GameDLLBundlePath = "SData/" + GameDllName;

        }
    }
}