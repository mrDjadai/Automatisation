using UnityEngine;
using DG.Tweening;

public class ItemGenerator : Tickable
{
    [SerializeField] private Item generatedPrefab;
    [SerializeField] private float minDelay;
    [SerializeField] private ItemPoint point;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnDuration;
    [SerializeField] private float spawnHeight;
    [SerializeField] private AudioSource spawnSource;

    protected override void OnTick()
    {
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
