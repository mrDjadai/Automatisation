using UnityEngine;

public class WaterPipe : PeriodicalBreackable
{
    [SerializeField] private WaterPipePoint[] points;

    protected override void OnBreak()
    {
        points[Random.Range(0, points.Length)].Break();
    }

    protected override void OnRepair()
    {
    }
}
