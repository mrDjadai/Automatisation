using UnityEngine;

public class ItemColorizer : ObjectChanger
{
    [SerializeField] private int colorId;
    [SerializeField] private float changingTime;

    protected override Item GetNewItem(Item old)
    {
        old.GetComponent<Colorizable>().Colorize(changingTime, colorId);
        return old;
    }
}
