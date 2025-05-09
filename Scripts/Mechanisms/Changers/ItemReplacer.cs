using UnityEngine;

public class ItemReplacer : ObjectChanger
{
    [SerializeField] private Item newItem;

    protected override Item GetNewItem(Item old)
    {
        Item nItem = Instantiate(newItem, old.transform.position, old.transform.rotation);
        nItem.Init(settings);
        Destroy(old.gameObject);
        return nItem;
    }
}
