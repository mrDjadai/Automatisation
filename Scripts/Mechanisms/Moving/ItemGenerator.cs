using UnityEngine;
using DG.Tweening;

public class ItemGenerator : Tickable, IItemConnectable
{
    [SerializeField] private Item generatedPrefab;
    [SerializeField] private float minDelay;
    [SerializeField] private ItemPoint point;
    [SerializeField] private ItemPoint output;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnDuration;
    [SerializeField] private float spawnHeight;
    [SerializeField] private AudioSource spawnSource;

    public void ConnectToInput(ItemPoint innerPoint, ItemPoint outerPoint)
    {
        if (innerPoint != point)
        {
            Debug.LogError("Соединение не с той точкой");
            return;
        }
        output = outerPoint;
    }

    protected override void OnTick()
    {
        point.Move(output);

        if (point.IsEmpty)
        {
            Item item = Instantiate(generatedPrefab, spawnPoint.position + Vector3.up * spawnHeight, spawnPoint.rotation);
            item.Init(settings);

            item.transform.DOMove(spawnPoint.position, spawnDuration).OnComplete(() => {
                item.Move(point);
                spawnSource.Play();
            });
        }
    }

}
