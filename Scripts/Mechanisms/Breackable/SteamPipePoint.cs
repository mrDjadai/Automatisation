using UnityEngine;

public class SteamPipePoint : MonoBehaviour, ILookDetectable
{
    [SerializeField] private SteamPipe pipe;
    [SerializeField] private float timeToRepair;
    [SerializeField] private string repairSpeedKey;
    [SerializeField] private string autoKey;
    [SerializeField] private float repairSpeedBonus;
    private bool autoMode;

    private void Start()
    {
        if (SaveManager.instance.HasUpgrade(repairSpeedKey))
        {
            timeToRepair /= repairSpeedBonus;
        }
        autoMode = SaveManager.instance.HasUpgrade(autoKey);
    }

    private bool isRepairing;

    private void Update()
    {
        if (isRepairing)
        {
            pipe.Repair(Time.deltaTime / timeToRepair, autoMode);
        }
    }


    public void OnLook()
    {
        isRepairing = true;
    }

    public void OnUnLook()
    {
        isRepairing = false;
    }

    public virtual void OnStartLook()
    {
        if (!autoMode)
        {
            return;
        }

        if (PlayerInventory.instance.InHandItem is Welding)
        {
            Welding w = PlayerInventory.instance.InHandItem as Welding;

            if (!w.IsActive)
            {
                w.Use();
            }
        }
    }

    public virtual void OnEndLook()
    {
        if (!autoMode)
        {
            return;
        }

        if (PlayerInventory.instance.InHandItem is Welding)
        {
            Welding w = PlayerInventory.instance.InHandItem as Welding;

            if (w.IsActive)
            {
                w.Use();
            }
        }
    }

    public virtual void Interact()
    {
    }

    public virtual void EndInteract()
    {
    }
}
