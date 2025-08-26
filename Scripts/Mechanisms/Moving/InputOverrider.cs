using UnityEngine;

public class InputOverrider : Tickable
{
    [SerializeField] private Item generatedPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private ItemPoint output;

    protected override void OnTick()
    {
        if (output.IsEmpty)
        {
            Item item = Instantiate(generatedPrefab, spawnPoint.position, spawnPoint.rotation);
            item.Init(settings);
            item.Move(output);
        }
    }
}
