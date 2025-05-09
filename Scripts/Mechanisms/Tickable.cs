using UnityEngine;
using Zenject;

[SelectionBase]
public abstract class Tickable : MonoBehaviour
{
    [SerializeField] private Breackable[] breackables;
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
        foreach (var item in breackables)
        {
            if (item.IsBroken)
            {
                return;
            }
        }
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
