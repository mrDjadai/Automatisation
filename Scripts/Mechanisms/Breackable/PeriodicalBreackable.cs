using UnityEngine;
using System.Collections;

public abstract class PeriodicalBreackable : Breackable
{
    private float minFirstPeriodPercent;
    private float minPeriod;
    private float periodOffset;

    protected virtual IEnumerator Start()
    {
        yield return new WaitUntil(Starter.IsStarted);

        float time;

        float minFirstPeriod = minFirstPeriodPercent * minPeriod;
        time = minFirstPeriod + Random.Range(0, minPeriod + periodOffset - minFirstPeriod);

        yield return new WaitForSeconds(time);
        Break();
        yield return new WaitWhile(() => { return IsBroken; });

        while (true)
        {
            time = minPeriod + Random.Range(0, periodOffset);

            yield return new WaitForSeconds(time);
            Break();
            yield return new WaitWhile(() => { return IsBroken; });
        }
    }

    protected override void OnLoadSettings(Vector3 data)
    {
        minFirstPeriodPercent = data.x;
        minPeriod = data.y;
        periodOffset = data.z;
    }
}
