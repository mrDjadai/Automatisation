using UnityEngine;

public class GearPlace : MonoBehaviour, ILookDetectable
{
    public Transform Point => transform;
    public bool IsEmpty => gear == null;
    public Gear PlacedGear => gear;
    [SerializeField] private Gear gear;
    [SerializeField] private GearManager manager;
    [SerializeField] private float rotatingSpeed;
    [SerializeField] private string autoKey;
    private bool automised;

    private void Awake()
    {
        automised = SaveManager.instance.HasUpgrade(autoKey);
    }

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

    public void OnStartLook()
    {
        if (!automised)
        {
            return;
        }

        if (gear != null && gear.IsBroken && PlayerInventory.instance.InHandItem == null)
        {
            gear.Interact();
        }
        else if(gear == null && PlayerInventory.instance.InHandItem is Gear && !(PlayerInventory.instance.InHandItem as Gear).IsBroken)
        {
            PlayerInventory.instance.InHandItem.Use();
        }
    }

    public void OnEndLook()
    {
    }

    public void Interact()
    {
    }

    public void EndInteract()
    {
    }
}
