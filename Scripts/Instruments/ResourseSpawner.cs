using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ResourseSpawner : MonoBehaviour
{
    [SerializeField] protected GameObject prefab;
    [SerializeField] private float respawnDistance;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int maxCount = 50;
    [SerializeField] private bool isLimitedMode;
    [SerializeField] private float spawnDelay;

    private List<Transform> spawned = new List<Transform>();
    private Transform lastSpawned;
    private bool staredSpawn;

    private void Start()
    {
        lastSpawned = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation).transform;
        lastSpawned.GetComponent<IResourse>().SetSpawner(this);
    }

    private IEnumerator Spawn()
    {
        staredSpawn = true;
        yield return new WaitForSeconds(spawnDelay);
        Start();
        staredSpawn = false;
    }

    private void Update()
    {
        if (staredSpawn || isLimitedMode && spawned.Count >= maxCount)
        {
            this.enabled = false;
            return;

        }
        if (Vector3.Distance(lastSpawned.position, spawnPoint.position) > respawnDistance)
        {
            spawned.Add(lastSpawned);
            CheckLength();
            StartCoroutine(Spawn());
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