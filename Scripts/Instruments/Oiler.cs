using UnityEngine;

public class Oiler : Instrument
{
    [SerializeField] private int maxUses;
    [SerializeField] private Transform raycastPoint;
    [SerializeField] private LayerMask raycastLayers;
    [SerializeField] private GameObject puddlePrefab;

    private int uses;

    private void Start()
    {
        uses = maxUses;
    }

    public override void Use()
    {
        base.Use();
        if (uses < 1)
        {
            return;
        }
        uses--;
        if (Physics.Raycast(raycastPoint.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, raycastLayers))
        {
            Instantiate(puddlePrefab, hit.point, Quaternion.identity);
        }
    }
}
