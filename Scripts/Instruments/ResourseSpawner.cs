using UnityEngine;

public class ResourseSpawner : MonoBehaviour
{
    [SerializeField] protected GameObject prefab;
    [SerializeField] private float respawnDistance;
    [SerializeField] private Transform spawnPoint;

    private Transform lastSpawned;

    private void Start()
    {
        lastSpawned = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation).transform;   
    }

    private void Update()
    {
        if (Vector3.Distance(lastSpawned.position, spawnPoint.position) > respawnDistance)
        {
            Start();
        }
    }
}
