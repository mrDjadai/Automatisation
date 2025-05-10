using UnityEngine;

public class Hose : Instrument
{
    [SerializeField] private HoseManager manager;
    [SerializeField] private Transform startPoint;
    [SerializeField] private float length;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private float targetDistance;
    [SerializeField] private float pushForce;
    [SerializeField] private ParticleSystem steam;
    [SerializeField] private float minY;
    [SerializeField, Range(0, 1)] private float returnPercent;
    [SerializeField] private EaseAudioSourse steamSource;

    [SerializeField] private Collider interactCollider;
    
    private bool inHand;
    private bool placed = true;
    private Transform tr;


    public void DropItem()
    {
        steam.Play();
        steamSource.Play();
        placed = false;
        interactCollider.enabled = true;
        rb.isKinematic = false;
        rb.AddForce(Random.onUnitSphere * pushForce, ForceMode.Impulse);
    }

    public override void Use()
    {
        base.Use();
        Ray ray = new Ray(Interactor.instance.CameraTransform.position, Interactor.instance.CameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform == targetPoint)
            {
                PlayerInventory.instance.DropItem();
                placed = true;
                interactCollider.enabled = false;

                StartCoroutine(GoToPoint(targetPoint, OnPlace));
            }
        }
    }

    private void OnPlace()
    {
        manager.Repair();
        steam.Stop();
        steamSource.Stop();
    }


    private void Start()
    {
        tr = transform;
    }

    protected override void OnDrop()
    {
        base.OnDrop();
        inHand = false;
        if (placed)
        {
            rb.isKinematic = true;
        }
    }

    protected override void OnTake()
    {
        base.OnTake();
        inHand = true;
    }

    private void Update()
    {
        if (!placed)
        {
            if (inHand)
            {
                if (Vector3.Distance(startPoint.position, tr.position) > length)
                {
                    PlayerInventory.instance.DropItem();
                    return;
                }
            }
            else
            {
                if (Vector3.Distance(tr.position, startPoint.position) > length)
                {
                    tr.position = startPoint.position + (tr.position - startPoint.position).normalized * length;
                }
                if (tr.position.y < minY)
                {
                    tr.position = new Vector3(tr.position.x, minY, tr.position.z);
                }
            }
            
        }
    }
}
