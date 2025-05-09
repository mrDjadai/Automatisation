using UnityEngine;

public class Welding : Instrument
{
    [SerializeField] private ParticleSystem particle;

    [SerializeField] private Transform raycastPoint;
    [SerializeField] private float raycastDistance;
    [SerializeField] private LayerMask raycsastLayers;

    private bool isActive;
    private SteamPipePoint point;

    public override void Use()
    {
        isActive = !isActive;
        if (isActive)
        {
            particle.Play();
        }
        else
        {
            particle.Stop();
        }
    }

    protected override void OnDrop()
    {
        base.OnDrop();
        if (isActive)
        {
            Use();
        }
    }


    private void Update()
    {
        if (isActive)
        {
            Ray ray = new Ray(Interactor.instance.CameraTransform.position, Interactor.instance.CameraTransform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance, raycsastLayers, QueryTriggerInteraction.Collide))
            {
                if (hit.rigidbody && hit.rigidbody.TryGetComponent<SteamPipePoint>(out SteamPipePoint p))
                {
                    point = p;
                    p.OnLook();
                }
            }
            else
            {
                if (point != null)
                {
                    point.OnUnLook();
                    point = null;
                }
            }
        }
        else
        {
            if (point != null)
            {
                point.OnUnLook();
                point = null;
            }
        }
    }
}
