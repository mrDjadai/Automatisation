using UnityEngine;

public class SteamPipePoint : MonoBehaviour
{
    [SerializeField] private SteamPipe pipe;
    [SerializeField] private float timeToRepair;

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
