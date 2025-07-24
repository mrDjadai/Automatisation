using UnityEngine;

public class GearRotator : MonoBehaviour
{
    [SerializeField] private Vector3 rotatingAxis;
    [SerializeField] private float rotatingSpeed;
    [SerializeField] private Transform[] gearsR;
    [SerializeField] private Transform[] gearsL;

    private float power = 1;

    public float Power
    {
        get
        {
            return power;
        }
        set
        {
            power = Mathf.Clamp01(value);
        }
    }

    private void Update()
    {
        float angle = rotatingSpeed * Time.deltaTime * power;

        foreach (var item in gearsR)
        {
            item.RotateAroundLocal(rotatingAxis, angle);
        }
        foreach (var item in gearsL)
        {
            item.RotateAroundLocal(rotatingAxis, -angle);
        }
    }
}
