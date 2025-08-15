using UnityEngine;

public class Tape : Instrument
{
    [SerializeField] private Transform visualisator;
    [SerializeField] private string[] bonusUsesKeys;

    [SerializeField] private GameObject repairTrigger;
    [SerializeField] private string autoKey;
    private int useCount = 1;

    private void Start()
    {
        foreach (var item in bonusUsesKeys)
        {
            if (SaveManager.instance.HasUpgrade(item))
            {
                useCount++;
            }
        }
        repairTrigger.SetActive(SaveManager.instance.HasUpgrade(autoKey));
    }

    public override void Use()
    {
        base.Use();
        Ray ray = new Ray(Interactor.instance.CameraTransform.position, Interactor.instance.CameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.TryGetComponent<WaterPipePoint>(out WaterPipePoint p))
            {
                Repair(p, hit.point, hit.normal);
            }
        }
    }

    private void Repair(WaterPipePoint p, Vector3 point, Vector3 normal)
    {
        Transform instance = Instantiate(visualisator, point, Quaternion.identity);
        instance.up = normal;
        instance.RotateAroundLocal(Vector3.up, Random.Range(0, 2 * Mathf.PI));
        p.Repair(instance.gameObject);

        useCount--;

        if (useCount < 1)
        {
            Interactor.instance.DropItem();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }

        if (other.attachedRigidbody.TryGetComponent<WaterPipePoint>(out WaterPipePoint p))
        {
            Repair(p, p.transform.position, p.transform.forward);
        }
    }
}
