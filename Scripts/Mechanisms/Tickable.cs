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
    private bool addedEvent;

    [Inject]
    private void Construct(TickSetter t, GameSettings s)
    {
        tick = t;
        if (gameObject.activeSelf)
        {
            tick.OnTick += HandleTick;
            addedEvent = true;
        }
        settings = s;
    }


    private void Start()
    {
        if (!addedEvent)
        {
            tick.OnTick += HandleTick;
        }
    }

    public bool IsBroken()
    {
        foreach (var item in breackables)
        {
            if (item.IsBroken)
            {
                return true;
            }
        }
        return false;
    }

    protected virtual void OnDestroy()
    {
        try
        {
            tick.OnTick -= HandleTick;
        }
        catch (System.Exception)
        {
        }
    }

    private void HandleTick()
    {
        if (IsBroken())
        {
            return;
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
