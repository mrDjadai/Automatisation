using UnityEngine;

public class Welding : Instrument, IResourse
{
    public bool IsActive => isActive;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private EaseAudioSourse audioSourse;

    [SerializeField] private Transform raycastPoint;
    [SerializeField] private float raycastDistance;
    [SerializeField] private LayerMask raycsastLayers;

    [SerializeField] private float gearRotatingSpeed;
    [SerializeField] private Transform[] gearsUpRotater;
    [SerializeField] private float useTime;
    [SerializeField] private TimeUpgrade[] timeUpgrades;
    [SerializeField] private AudioSource useSource;
    [SerializeField] private AudioClip useClip;
    [SerializeField] private AudioClip unUseClip;
    [SerializeField] private AudioClip cantUseClip;
    [SerializeField] private float distanceBonus;
    [SerializeField] private string distanceKey; 
    [SerializeField] private string infinityKey;

    private bool isActive;
    private SteamPipePoint point;
    private float lifeTime;
    private bool isInfinity;


    private ResourseSpawner resourseSpawner;

    public void SetSpawner(ResourseSpawner spawner)
    {
        resourseSpawner = spawner;
    }

    public void OnGarbageDestroy()
    {
        resourseSpawner.RemoveFromList(transform);
    }

    protected override void Awake()
    {
        base.Awake();

        foreach (var item in timeUpgrades)
        {
            if (SaveManager.instance.HasUpgrade(item.key))
            {
                useTime *= item.multiplier;
            }
        }
        if (SaveManager.instance.HasUpgrade(distanceKey))
        {
            particle.transform.localScale = new Vector3(1, particle.transform.localScale.y * distanceBonus, 1);
            raycastDistance *= distanceBonus;
        }

        isInfinity = SaveManager.instance.HasUpgrade(infinityKey);
    }

    public override void Use()
    {
        isActive = !isActive;

        if (!isInfinity && lifeTime >= useTime)
        {
            isActive = false;
            useSource.PlayOneShot(cantUseClip);
        }

        if (isActive)
        {
            particle.Play();
            audioSourse.Play();
            useSource.PlayOneShot(useClip);
        }
        else
        {
            particle.Stop();
            audioSourse.Stop();
            useSource.PlayOneShot(unUseClip);
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
      //  particle.gameObject.layer = 0;
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

            lifeTime += Time.deltaTime;
            if (lifeTime >= useTime)
            {
                Use();
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

    [System.Serializable]
    private struct TimeUpgrade
    {
        public string key;
        public float multiplier;
    }
}
