using UnityEngine;

[CreateAssetMenu(menuName = "Game/LevelData", fileName = "LevelData")]
public class LevelData : ScriptableObject
{
    [field: SerializeField] public Difficult difficult { get; private set; }
    [field: SerializeField] public float duration { get; private set; }
    [field: SerializeField] public ItemData[] items { get; private set; }
    [field : SerializeField] public Sprite gazete { get; private set; }


    [System.Serializable]
    public struct ItemData
    {
        public string nameKey;
        public int id;
        public int targetCount;
    }
}
