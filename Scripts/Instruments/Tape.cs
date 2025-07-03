using UnityEngine;

public class Tape : Instrument
{
    [SerializeField] private Transform visualisator;

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
                Interactor.instance.DropItem();
                Destroy(gameObject);
            }
        }
    }

}
