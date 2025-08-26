using UnityEngine;
using Zenject;

[SelectionBase]
public abstract class Breackable : MonoBehaviour
{
    private const string indicatorKey = "indicator";

    [SerializeField] private int difficultDataId;
    [SerializeField] private bool breakeOnAwake;
    [SerializeField] private GameObject indicator;
    [SerializeField] private bool canBrakeMore;

    public bool IsBroken => isBroken;
    protected bool isBroken;

    private LevelStarter levelStarter;
    private BreakManager breakManager;

    protected LevelStarter Starter => levelStarter;
    protected BreakManager BreakManager => breakManager;

    private bool useIndicator;

    [Inject]
    private void Construct(LevelStarter l, BreakManager b)
    {
        levelStarter = l;
        breakManager = b;

        OnLoadSettings(levelStarter.CurrentDifficult.GetDifficultData(difficultDataId));

        if (breakeOnAwake)
        {
            BreakOnAwake();
        }
        useIndicator = SaveManager.instance.HasUpgrade(indicatorKey);

        if (indicator != null)
        {
            indicator.SetActive(false);
        }
    }

    public void Break()
    {
        if (!canBrakeMore && isBroken)
        {
            return;
        }
        isBroken = true;
        OnBreak();
        if (useIndicator)
        {
            indicator.SetActive(true);
        }
        breakManager.OnBreak();
    }

    protected virtual void BreakOnAwake()
    {
        Break();
    }

    public void Repair()
    {
        if (!isBroken)
        {
            return;
        }
        isBroken = false;
        OnRepair();
        if (useIndicator)
        {
            indicator.SetActive(true);
        }
        breakManager.OnRepair();
    }

    protected abstract void OnBreak();
    protected abstract void OnRepair();

    protected abstract void OnLoadSettings(Vector3 data);
}
