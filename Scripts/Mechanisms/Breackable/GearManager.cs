using UnityEngine;

public class GearManager : PeriodicalBreackable
{
    [SerializeField] private GearPlace[] places;

    public void Check()
    {
        foreach (var item in places)
        {
            if (item.IsBroken())
            {
                Break();
                return;
            }
        }
        Repair();
    }

    protected override void OnBreak()
    {
        places[Random.Range(0, places.Length)].TryBreak();
    }

    protected override void OnRepair()
    {
    }

    private void Update()
    {
        if (IsBroken == false)
        {
            foreach (var item in places)
            {
                item.Rotate();
            }
        }
    }
}
