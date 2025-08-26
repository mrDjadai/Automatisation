using UnityEngine;
using Zenject;

public class OutputOverrider : Tickable
{
    [SerializeField] private ItemConveer conveer;
    [SerializeField] private ItemPoint input;
    private ItemsManager manager;

    [Inject]
    private void Construct(ItemsManager itemsManager)
    {
        manager = itemsManager;
    }

    private void Start()
    {
        conveer.OverideOutput(input);
    }

    protected override void OnTick()
    {
        if (input.IsEmpty == false)
        {
            Item i = input.Pop();
            manager.Add(i.ID, i.ColorID);

            Destroy(i.gameObject);
        }
    }
}
