namespace LD
{
    public static partial class RuntimeSettings
    {
#if !UNITY_EDITOR
        public static bool HybridCLREnable = true;
#else
        public static bool HybridCLREnable = false;
#endif
    }
}