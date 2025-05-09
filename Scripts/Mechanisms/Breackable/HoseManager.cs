using UnityEngine;

public class HoseManager : PeriodicalBreackable
{
    [SerializeField] private Hose hose;

    protected override void OnBreak()
    {
        hose.Drop();
    }

    protected override void OnRepair()
    {

    }
}
