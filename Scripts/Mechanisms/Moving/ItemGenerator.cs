using UnityEngine;

public class ItemGenerator : Tickable
{
    [SerializeField] private Item generatedPrefab;
    [SerializeField] private float minDelay;
    [SerializeField] private ItemPoint point;
    [SerializeField] private Transform spawnPoint;

    protected override void OnTick()
    {
        if (point.IsEmpty)
        {
            Item item = Instantiate(generatedPrefab, spawnPoint.position, spawnPoint.rotation);
            item.Init(settings);
            item.Move(point);
        }
    }
}
