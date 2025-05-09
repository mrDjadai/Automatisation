using UnityEngine;

public class ItemConveer : Tickable
{
    [Header("Порядок по пути движения")]
    [SerializeField] private ItemPoint[] points;

    [SerializeField] private ItemPoint input;
    [SerializeField] private ItemPoint output;

    protected override void OnTick()
    {
        points[^1].Move(output);
        for (int i = points.Length - 2; i >= 0; i--)
        {
            points[i].Move(points[i + 1]);
        }
        if (input != null)
        {
            input.Move(points[0]);
        }
    }
}
