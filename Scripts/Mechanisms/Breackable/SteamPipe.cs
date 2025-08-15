using UnityEngine;
using System.Collections;

public class SteamPipe : PeriodicalBreackable
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private SteamPipePoint[] points;


    

    protected override void OnBreak()
    {
        SteamPipePoint point = null;
        foreach (var item in points)
        {
            if (item.ConnectedPoint == null)
            {
                point = item;
                break;
            }
        }

        if (point != null)
        {
            while (true)
            {
                Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
                foreach (var item in points)
                {
                    if (item.ConnectedPoint == spawn)
                    {
                        break;
                    }
                }
                point.ActivateOnPlace(spawn);
                return;
            }
        }
      
    }

    public void TryRepair()
    {
        foreach (var item in points)
        {
            if (item.IsBroken)
            {
                return;
            }
        }
        Repair();
    }

    protected override void OnRepair()
    {
        foreach (var item in points)
        {
            item.OnRepair();
        }
    }
}
