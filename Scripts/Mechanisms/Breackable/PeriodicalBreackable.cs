using UnityEngine;
using System.Collections;
using Zenject;

public abstract class PeriodicalBreackable : Breackable
{
    [SerializeField] private float minPeriod;
    [SerializeField] private float periodOffset;
    [SerializeField] private LevelStarter levelStarter;
    protected LevelStarter Starter => levelStarter;

    [Inject]
    private void Construct(LevelStarter l)
    {
        levelStarter = l;
    }

    protected virtual IEnumerator Start()
    {
        yield return new WaitUntil(levelStarter.IsStarted);

        float time;

        time = Random.Range(0, minPeriod + periodOffset);

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
}
