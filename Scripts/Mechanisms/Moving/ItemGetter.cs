using UnityEngine;
using Zenject;

public class ItemGetter : Tickable
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
            input.Move(endPoint);
            if (getSource.isPlaying == false)
            {
                getSource.Play();

            }
        }
    }
}
