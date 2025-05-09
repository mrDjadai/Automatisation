using UnityEngine;
using Zenject;

public class ItemGetter : Tickable
{
    [SerializeField] private ItemPoint input;
    [SerializeField] private ItemPoint endPoint;
    private ItemsManager manager;

    [Inject]
    private void Construct(ItemsManager itemsManager)
    {
        manager = itemsManager;
    }

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
            manager.Add(item.ID, item.ColorID);
            item.Move(endPoint);
        }
    }
}
