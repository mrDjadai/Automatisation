using UnityEngine;

[CreateAssetMenu(menuName = "Game/Settings", fileName = "Settings")]
public class GameSettings : ScriptableObject
{
    public float TickTime => tickTime;
    public Color[] Colors => colors;
    [SerializeField] private float tickTime;
    [SerializeField] private Color[] colors;
}
