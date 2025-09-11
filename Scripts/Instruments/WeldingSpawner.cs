using UnityEngine;

public class WeldingSpawner : ResourseSpawner
{
    [SerializeField] private Animator animator;
    [SerializeField] private MeshRenderer lamp;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;

    private bool stopped;

    protected void Awake()
    {
        lamp.material.SetColor("_BaseColor", inactiveColor);
        lamp.material.SetColor("_EmissionColor", inactiveColor);
    }

    protected override void HandleSpawn(Transform item)
    {
        base.HandleSpawn(item);
        item.GetComponent<Rigidbody>().isKinematic = true;
        item.parent = spawnPoint;
    }

    public void OnSpawn()
    {
        animator.speed = 0;
        stopped = true;
        lamp.material.SetColor("_BaseColor", activeColor);
        lamp.material.SetColor("_EmissionColor", activeColor);
    }

    protected override void Update()
    {
        base.Update();
        if (stopped && lastSpawned == null)
        {
            animator.speed = 1;
            lamp.material.SetColor("_BaseColor", inactiveColor);
            lamp.material.SetColor("_EmissionColor", inactiveColor);
            stopped = false;
        }
    }
}
