using UnityEngine;
using Zenject;

public abstract class Tickable : MonoBehaviour
{
    [SerializeField, Min(1)] private int tickForUse = 1;
    protected GameSettings settings;
    private TickSetter tick;
    private int curTick;

    [Inject]
    private void Construct(TickSetter t, GameSettings s)
    {
        tick = t;
        tick.OnTick += HandleTick;
        settings = s;
    }

    protected virtual void OnDestroy()
    {
        tick.OnTick -= HandleTick;
    }

    private void HandleTick()
    {
        if (tickForUse == 1)
        {
            OnTick();
            return;
        }
        curTick++;
        if (curTick >= tickForUse)
        {
            OnTick();
            curTick = 0;
        }
    }

    protected abstract void OnTick();
}
