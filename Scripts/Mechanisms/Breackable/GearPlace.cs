using UnityEngine;

public class GearPlace : MonoBehaviour
{
    public Transform Point => transform;
    public bool IsEmpty => gear == null;
    [SerializeField] private Gear gear;
    [SerializeField] private GearManager manager;

    public bool IsBroken()
    {
        return IsEmpty || gear.IsBroken;
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
        if (gear != null)
        {
            gear.GetComponent<Rigidbody>().isKinematic = true;
        }
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
