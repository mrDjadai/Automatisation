using UnityEngine;

public class ResourseSpawnerVariant : ResourseSpawner
{
    [SerializeField] private Variant[] variants;

    private void Awake()
    {
        for (int i = variants.Length - 1; i >= 0; i--)
        {
            if (SaveManager.instance.HasUpgrade(variants[i].key))
            {
                prefab = variants[i].prefab;
                return;
            }
        }
    }

    [System.Serializable]
    private struct Variant
    {
        public string key;
        public GameObject prefab;
    }
}
