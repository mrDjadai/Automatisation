using UnityEngine;
using Zenject;

public class ItemGetter : Tickable, IItemConnectable
{
    [SerializeField] private ItemPoint input;
    [SerializeField] private ItemPoint endPoint;
    [SerializeField] private AudioSource getSource;

    private ItemsManager manager;

    [Inject]
    private void Construct(ItemsManager itemsManager)
    {
        manager = itemsManager;
    }

    public void ConnectToInput(ItemPoint innerPoint, ItemPoint outerPoint)
    {
        Debug.LogError("У данного типа конвейера не должно быть точки вы");
    }

    protected override void OnTick()
    {
        if (endPoint.IsEmpty == false)
        {
            Item i = endPoint.Pop();
            manager.Add(i.ID, i.ColorID);

            Destroy(i.gameObject);
        }

        if (input.IsEmpty == false)
        {
            bool moved = input.Move(endPoint);
            if (moved && getSource.isPlaying == false)
            {
                getSource.Play();
            }
        }
    }
}
