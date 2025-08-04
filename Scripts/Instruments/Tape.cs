using UnityEngine;

public class Tape : Instrument
{
    [SerializeField] private Transform visualisator;
    [SerializeField] private string[] bonusUsesKeys;

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
    }

    public override void Use()
    {
        base.Use();
        Ray ray = new Ray(Interactor.instance.CameraTransform.position, Interactor.instance.CameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.TryGetComponent<WaterPipePoint>(out WaterPipePoint p))
            {
                Transform instance = Instantiate(visualisator, hit.point, Quaternion.identity);
                instance.up = hit.normal;
                instance.RotateAroundLocal(Vector3.up, Random.Range(0, 2 * Mathf.PI));
                p.Repair(instance.gameObject);

                useCount--;

                if (useCount < 1)
                {
                    Interactor.instance.DropItem();
                    Destroy(gameObject);
                }
            }
        }
    }

}
