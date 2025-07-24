using UnityEngine;

public class Welding : Instrument
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private EaseAudioSourse audioSourse;

    [SerializeField] private Transform raycastPoint;
    [SerializeField] private float raycastDistance;
    [SerializeField] private LayerMask raycsastLayers;

    [SerializeField] private float gearRotatingSpeed;
    [SerializeField] private Transform[] gearsUpRotater;

    private bool isActive;
    private SteamPipePoint point;

    public override void Use()
    {
        isActive = !isActive;
        if (isActive)
        {
            particle.Play();
            audioSourse.Play();
        }
        else
        {
            particle.Stop();
            audioSourse.Stop();
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

    protected override void OnTake()
    {
        base.OnTake();
        particle.gameObject.layer = 0;
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

            float angle = gearRotatingSpeed * Time.deltaTime;      
            foreach (var item in gearsUpRotater)
            {
                item.RotateAroundLocal(Vector3.up, angle);
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
