using UnityEngine;

[CreateAssetMenu(menuName = "Game/Difficult", fileName = "Difficult")]

public class Difficult : ScriptableObject
{
    [SerializeField] private Data[] values;

    public Vector3 GetDifficultData(int id)
    {
        return new Vector3(values[id].minFirstPeriodPercent,
                           values[id].minPeriod,
                           values[id].periodOffset);
    }    

    [System.Serializable]
    private struct Data
    {
        [SerializeField] private string header;
        public float minFirstPeriodPercent;
        public float minPeriod;
        public float periodOffset;
    }
}
