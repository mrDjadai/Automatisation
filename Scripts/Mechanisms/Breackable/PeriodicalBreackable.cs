using UnityEngine;
using System.Collections;

public abstract class PeriodicalBreackable : Breackable
{
    [SerializeField] private UpgradeBonus[] upgrades;

    private float minFirstPeriodPercent;
    private float minPeriod;
    private float periodOffset;

    private float timeMultiplier = 1;


    protected virtual IEnumerator Start()
    {
        foreach (var item in upgrades)
        {
            if (SaveManager.instance.HasUpgrade(item.key))
            {
                timeMultiplier += item.bonus;
            }
        }

        yield return new WaitUntil(Starter.IsStarted);

        float time;

        float minFirstPeriod = minFirstPeriodPercent * minPeriod;
        time = minFirstPeriod + Random.Range(0, minPeriod + periodOffset - minFirstPeriod);
        yield return new WaitForSeconds(time * timeMultiplier);

        yield return new WaitForSeconds(time * timeMultiplier * (BreakManager.DelayMultiplier - 1));

        Break();

        yield return new WaitWhile(() => { return IsBroken; });


        while (true)
        {
            time = minPeriod + Random.Range(0, periodOffset);

            yield return new WaitForSeconds(time * timeMultiplier);
            yield return new WaitForSeconds(time * timeMultiplier * (BreakManager.DelayMultiplier - 1));

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

    [System.Serializable]
    private struct UpgradeBonus
    {
        public string key;
        [Range(0, 1) ]public float bonus;
    }
}
