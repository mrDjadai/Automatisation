using UnityEngine;
using Zenject;

[SelectionBase]
public abstract class Breackable : MonoBehaviour
{
    [SerializeField] private int difficultDataId;
    public bool IsBroken => isBroken;
    protected bool isBroken;

    private LevelStarter levelStarter;
    protected LevelStarter Starter => levelStarter;

    [Inject]
    private void Construct(LevelStarter l)
    {
        levelStarter = l;
        OnLoadSettings(levelStarter.CurrentDifficult.GetDifficultData(difficultDataId));
    }

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

    protected abstract void OnLoadSettings(Vector3 data);
}
