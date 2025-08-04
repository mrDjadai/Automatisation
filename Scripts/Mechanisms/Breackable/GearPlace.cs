using UnityEngine;

public class GearPlace : MonoBehaviour
{
    public Transform Point => transform;
    public bool IsEmpty => gear == null;
    [SerializeField] private Gear gear;
    [SerializeField] private GearManager manager;
    [SerializeField] private float rotatingSpeed;

    public bool IsBroken()
    {
        return IsEmpty || gear.IsBroken;
    }

    public void Rotate()
    {
        transform.RotateAround(transform.forward, rotatingSpeed * Time.deltaTime);
        if (gear != null)
        {
            gear.transform.RotateAround(transform.forward, rotatingSpeed * Time.deltaTime);
        }
    }

    public bool TryBreak()
    {
        if (IsEmpty)
        {
            return false;
        }
        gear.Break();
        return true;
    }

    public void Take()
    {
    /*    if (gear != null)
        {
            gear.GetComponent<Rigidbody>().isKinematic = true;
        }*/
        gear = null;
        manager.Check();
    }

    public void Place(Gear g)
    {
        gear = g;
        g.GetComponent<Rigidbody>().isKinematic = true;
        manager.Check();
    }
}
