using UnityEngine;
using System.Collections.Generic;

public class ItemGetter : Tickable
{
    [SerializeField] private ItemPoint input;
    [SerializeField] private ItemPoint endPoint;
    private Dictionary<int, int> count = new Dictionary<int, int>();

    protected override void OnTick()
    {
        if (endPoint.IsEmpty == false)
        {
            Item i = endPoint.Pop();
            Destroy(i.gameObject);
        }

        Item item = input.Pop();
        if (item != null)
        {
            if (count.ContainsKey(item.ID))
            {
                count[item.ID]++;
            }
            else
            {
                count[item.ID] = 1;
            }
            item.Move(endPoint);
        }
    }
}
