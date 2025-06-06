using UnityEngine;
using System.Collections;

public abstract class PeriodicalBreackable : Breackable
{
    [SerializeField] private float minPeriod;
    [SerializeField] private float periodOffset;

    protected virtual IEnumerator Start()
    {
        while (true)
        {
            float time = minPeriod + Random.Range(0, periodOffset);
            yield return new WaitForSeconds(time);
            Break();
            yield return new WaitWhile(() => { return IsBroken; });
        }
    }
}
