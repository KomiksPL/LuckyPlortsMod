using UnityEngine;
using Object = UnityEngine.Object;

namespace LuckyPlorts
{
    public static class PrefabUtils
    {
        private static Transform runtimePrefab;

        static PrefabUtils()
        {
            InitializeRuntimePrefab();
        }

        public static GameObject CopyPrefab(GameObject prefab)
        {
            return Object.Instantiate(prefab, runtimePrefab);
        }

        private static void InitializeRuntimePrefab()
        {
            runtimePrefab = new GameObject("RuntimePrefab").transform;
            runtimePrefab.gameObject.SetActive(false);
            Object.DontDestroyOnLoad(runtimePrefab.gameObject);
            runtimePrefab.gameObject.hideFlags |= HideFlags.HideAndDontSave;
        }
    }
}