using UnityEngine;

public class BreakManager : MonoBehaviour
{
    public float DelayMultiplier { get; private set; }

    [SerializeField] private AnimationCurve countToDelay;

    private int count;

    public void OnBreak()
    {
        count++;
        Recalculate();
    }

    public void OnRepair()
    {
        count--;
        Recalculate();
    }

    private void Recalculate()
    {
        DelayMultiplier = countToDelay.Evaluate(count);
    }
}
