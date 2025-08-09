using UnityEngine;

public class SteamPipePoint : MonoBehaviour
{
    [SerializeField] private SteamPipe pipe;
    [SerializeField] private float timeToRepair;
    [SerializeField] private string repairSpeedKey;
    [SerializeField] private float repairSpeedBonus;

    private void Start()
    {
        if (SaveManager.instance.HasUpgrade(repairSpeedKey))
        {
            timeToRepair /= repairSpeedBonus;
        }
    }

    private bool isRepairing;

    private void Update()
    {
        if (isRepairing)
        {
            pipe.Repair(Time.deltaTime / timeToRepair);
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
}
