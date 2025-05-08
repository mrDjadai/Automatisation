using UnityEngine;

[CreateAssetMenu(menuName = "Game/Settings", fileName = "Settings")]
public class GameSettings : ScriptableObject
{
    public float TickTime => tickTime;
    [SerializeField] private float tickTime;
}
