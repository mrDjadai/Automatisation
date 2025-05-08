using UnityEngine;

public class ItemPoint : MonoBehaviour
{
    public bool IsEmpty => curItem == null;
    public Transform Point => tr;
    private Item curItem;
    private Transform tr;

    private void Awake()
    {
        tr = transform;    
    }

    public bool Push(Item i)
    {
        if (curItem == null)
        {
            curItem = i;
            return true;
        }
        return false;
    }

    public Item Pop()
    {
        Debug.Log("pop");
        Item res = curItem;
        curItem = null;
        return res;
    }

    public bool Move(ItemPoint point)
    {
        if (curItem != null && point != null)
        {
            return curItem.Move(point);
        }
        return false;
    }
}
