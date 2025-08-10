using System.Collections;
using UnityEngine;

public class GearManager : PeriodicalBreackable
{
    [SerializeField] private GearPlace[] places;
    [SerializeField] private float repairDelay;
    [SerializeField] private GearPlace repairPlace;
    [SerializeField] private string selfRepairKey;

    private bool useSelfRepair;

    private void Awake()
    {
        useSelfRepair = SaveManager.instance.HasUpgrade(selfRepairKey);
    }

    public void Check()
    {
        foreach (var item in places)
        {
            if (item.IsBroken())
            {
                isBroken = true;
                return;
            }
        }
        Repair();
    }

    protected override void OnBreak()
    {
        places[Random.Range(0, places.Length)].TryBreak();
        if (useSelfRepair)
        {
            StartCoroutine(SelfRepair());
        }
    }

    private IEnumerator SelfRepair()
    {
        yield return new WaitForSeconds(repairDelay);
        if (!repairPlace.IsEmpty)
        {
            foreach (var item in places)
            {
                if (!item.IsEmpty && item.PlacedGear.IsBroken)
                {
                    Gear gear = repairPlace.PlacedGear;
                    repairPlace.Take();

                    item.PlacedGear.DropFromPlace();
                    item.Place(gear);
                    gear.transform.position = item.Point.position;
                    gear.transform.rotation = item.Point.rotation;
                    break;
                }
            }
        }
    }    

    protected override void OnRepair()
    {
    }

    private void Update()
    {
        if (IsBroken == false && Starter.IsStarted())
        {
            foreach (var item in places)
            {
                item.Rotate();
            }
        }
    }
}
