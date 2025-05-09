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

    [SerializeField] private Collider interactCollider;
    
    private Rigidbody rb;
    private bool inHand;
    private bool placed = true;
    private Transform tr;

    private Vector3 lookPoint;

    public void Drop()
    {
        steam.Play();
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
                    tr.position = startPoint.position + (lookPoint - startPoint.position).normalized * length;
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
                    Interactor.instance.InteractionEnd();

                }
            }
            else
            {
                if (Vector3.Distance(tr.position, startPoint.position) > length)
                {
                    tr.position = startPoint.position + (tr.position - startPoint.position).normalized * length;
                }
            }
            
        }
    }
}
