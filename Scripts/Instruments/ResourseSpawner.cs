using System.Collections.Generic;
using UnityEngine;

public class ResourseSpawner : MonoBehaviour
{
    [SerializeField] protected GameObject prefab;
    [SerializeField] private float respawnDistance;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int maxCount = 50;
    [SerializeField] private bool isLimitedMode;

    private List<Transform> spawned = new List<Transform>();
    private Transform lastSpawned;

    private void Start()
    {
        lastSpawned = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation).transform;
        lastSpawned.GetComponent<IResourse>().SetSpawner(this);
    }

    private void Update()
    {
        if (isLimitedMode && spawned.Count >= maxCount)
        {
            this.enabled = false;
            return;

        }
        if (Vector3.Distance(lastSpawned.position, spawnPoint.position) > respawnDistance)
        {
            spawned.Add(lastSpawned);
            CheckLength();
            Start();
        }
    }

    private void CheckLength()
    {
        if (spawned.Count > maxCount)
        {
            Destroy(spawned[0].gameObject);
            spawned.RemoveAt(0);
        }
    }

    public void RemoveFromList(Transform t)
    {
        if (isLimitedMode)
        {
            return;
        }
        if (spawned.Contains(t))
        {
            spawned.Remove(t);
        }
    }

    public void AddToList(Transform t)
    {
        if (isLimitedMode)
        {
            return;
        }

        if (!spawned.Contains(t))
        {
            spawned.Add(t);
            CheckLength();
        }
    }
}