using UnityEngine;
using DG.Tweening;

public class Oiler : Instrument
{
    [SerializeField] private int maxUses;
    [SerializeField] private Transform raycastPoint;
    [SerializeField] private LayerMask raycastLayers;
    [SerializeField] private GameObject puddlePrefab;
    [SerializeField] private ParticleSystem useParticles;
    [SerializeField] private Transform rotatable;
    [SerializeField] private Vector3 targetAngle;
    [SerializeField] private float animationDuration;

    private int uses;

    private void Start()
    {
        uses = maxUses;
    }

    private void Update()
    {
        useParticles.transform.forward = Vector3.down;
        useParticles.gameObject.layer = 0;
    }

    public override void Use()
    {
        base.Use();
        if (uses < 1)
        {
            return;
        }
        uses--;
        useParticles.Play();
        if (Physics.Raycast(raycastPoint.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, raycastLayers))
        {
            rotatable.DOLocalRotate(targetAngle, animationDuration / 2).OnComplete(() => {
                rotatable.DOLocalRotate(Vector3.zero, animationDuration / 2);
                Instantiate(puddlePrefab, hit.point, Quaternion.identity);
            });
        }
        else
        {
            rotatable.DOLocalRotate(targetAngle, animationDuration / 2).OnComplete(() => {
                rotatable.DOLocalRotate(Vector3.zero, animationDuration / 2);
            });
        }
    }
}
