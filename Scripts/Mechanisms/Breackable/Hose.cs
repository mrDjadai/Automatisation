using UnityEngine;

public class Hose : Interactable
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
    
    private Rigidbody rb;
    private bool inHand;
    private bool placed = true;
    private Transform tr;

    private Vector3 lookPoint;

    public void Drop()
    {
        steam.Play();
        steamSource.Play();
        placed = false;
        interactCollider.enabled = true;
        rb.isKinematic = false;
        rb.AddForce(Random.onUnitSphere * pushForce, ForceMode.Impulse);
    }

    private void Awake()
    {
        tr = transform;
        rb = GetComponent<Rigidbody>();
    }

    public override void EndInteract()
    {
        inHand = false;
        if (!placed)
        {
            rb.isKinematic = false;
        }
    }

    public override void Interact()
    {
        inHand = true;
        rb.isKinematic = true;
    }

    private void Update()
    {
        if (!placed)
        {
            if (inHand)
            {
                foreach (var item in Physics.RaycastAll(Interactor.instance.CameraTransform.position, Interactor.instance.CameraTransform.forward))
                {
                    if ((item.rigidbody == null) || (item.rigidbody != rb.gameObject))
                    {
                        lookPoint = item.point;
                        break;
                    }
                }
                if (Vector3.Distance(startPoint.position, lookPoint) > length)
                {
                    tr.position = startPoint.position + (lookPoint - startPoint.position).normalized * length * returnPercent;
                    Interactor.instance.InteractionEnd();
                }
                else
                {
                    tr.position = lookPoint;
                }

                if (Vector3.Distance(tr.position, targetPoint.position) < targetDistance)
                {
                    tr.position = targetPoint.position;
                    placed = true;
                    manager.Repair();
                    interactCollider.enabled = false;
                    steam.Stop();
                    steamSource.Stop();

                    Interactor.instance.InteractionEnd();

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
