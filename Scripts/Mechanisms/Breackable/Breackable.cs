using UnityEngine;

public abstract class Breackable : MonoBehaviour
{
    public bool IsBroken => isBroken;
    protected bool isBroken;

    public void Break()
    {
        if (isBroken)
        {
            return;
        }
        isBroken = true;
        OnBreak();
    }

    public void Repair()
    {
        if (!isBroken)
        {
            return;
        }
        isBroken = false;
        OnRepair();
    }

    protected abstract void OnBreak();
    protected abstract void OnRepair();

}
