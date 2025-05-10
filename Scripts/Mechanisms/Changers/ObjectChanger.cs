using UnityEngine;

public abstract class ObjectChanger : Tickable
{
    [SerializeField] private ItemPoint input;
    [SerializeField] private ItemPoint center;
    [SerializeField] private ItemPoint output;
    [SerializeField, Min(2)] private int tickToChange;
    [SerializeField] private int applyTick;
    private int tickAfterChange;
    private bool isChanginging;

    protected virtual void OnChangeStart()
    {

    }

    protected virtual void OnChangeEnd()
    {

    }

    protected override void OnTick()
    {
        if (isChanginging)
        {
            if (center.IsEmpty == false)
            {
                tickAfterChange++;
                if (tickAfterChange >= tickToChange)
                {
                    isChanginging = false;
                    tickAfterChange = 0;
                    OnChangeEnd();
                    return;
                }
                if (tickAfterChange == applyTick)
                {
                    GetNewItem(center.Pop()).Move(center);
                }
            }
        }
        else
        {
            center.Move(output);
            isChanginging = input.Move(center);
            if (isChanginging)
            {
                OnChangeStart();
            }
        }
    }

    protected abstract Item GetNewItem(Item old);
}
