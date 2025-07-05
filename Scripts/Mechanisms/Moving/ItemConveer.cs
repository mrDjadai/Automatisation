using UnityEngine;

public class ItemConveer : Tickable, IItemConnectable
{
    [Header("Порядок по пути движения")]
    [SerializeField] private ItemPoint[] points;

    [SerializeField] private ItemPoint output;


    public void ConnectToInput(ItemPoint innerPoint, ItemPoint outerPoint)
    {
        if (innerPoint != points[^1])
        {
            Debug.LogError("Соединение не с той точкой");
            return;
        }
        output = outerPoint;
    }

    protected override void OnTick()
    {
        points[^1].Move(output);
        for (int i = points.Length - 2; i >= 0; i--)
        {
            points[i].Move(points[i + 1]);
        }
    }
}
