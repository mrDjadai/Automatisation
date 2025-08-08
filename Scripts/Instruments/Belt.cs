using UnityEngine;

public class Belt : Instrument, IResourse
{
    [SerializeField] private LineRenderer renderer1;
    [SerializeField] private LineRenderer renderer2;


    private BeltPoint point1;
    private bool inHand;

    private ResourseSpawner resourseSpawner;

    public void SetSpawner(ResourseSpawner spawner)
    {
        resourseSpawner = spawner;
    }

    public override void Use()
    {
        base.Use();
        Ray ray = new Ray(Interactor.instance.CameraTransform.position, Interactor.instance.CameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.rigidbody && hit.rigidbody.TryGetComponent<BeltPoint>(out BeltPoint p))
            {
                if (p.IsBroken == false)
                {
                    return;
                }
                if (point1 == null)
                {
                    point1 = p;
                    p.PlaySound();
                    renderer1.enabled = true;
                    renderer2.enabled = true;

                    renderer1.SetPosition(0, p.Points[0].position);
                    renderer2.SetPosition(0, p.Points[1].position);
                }
                else if(point1.IsPare(p))
                {
                    point1.Repair();
                    p.PlaySound();
                    Interactor.instance.DropItem();
                    resourseSpawner.RemoveFromList(transform);
                    Destroy(gameObject);
                }
            }
        }
    }

    public void OnGarbageDestroy()
    {
        resourseSpawner.RemoveFromList(transform);
    }

    protected override void OnTake()
    {
        base.OnTake();
        inHand = true;
    }

    private void Update()
    {
        if (inHand)
        {
            renderer1.SetPosition(1, renderer1.transform.position);
            renderer2.SetPosition(1, renderer2.transform.position);
        }
    }

    protected override void OnDrop()
    {
        base.OnDrop();
        renderer1.enabled = false;
        renderer2.enabled = false;
        inHand = false;


        point1 = null;
    }
}
