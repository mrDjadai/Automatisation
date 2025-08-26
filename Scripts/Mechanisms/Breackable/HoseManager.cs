using UnityEngine;

public class HoseManager : PeriodicalBreackable
{
    [SerializeField] private Hose hose;
    [SerializeField] private AudioSource repairSource;

    protected override void OnBreak()
    {
        hose.DropItem();
    }

    protected override void OnRepair()
    {
        repairSource.Play();
    }
}
