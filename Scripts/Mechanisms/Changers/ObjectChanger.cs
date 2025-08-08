using UnityEngine;

public abstract class ObjectChanger : Tickable, IItemConnectable
{
    [SerializeField] private ItemPoint input;
    [SerializeField] private ItemPoint center;
    [SerializeField] private ItemPoint output;
    [SerializeField] private ItemPoint publicOutput;
    [SerializeField, Min(2)] private int tickToChange;
    [SerializeField] private int applyTick;
    private int tickAfterChange;
    private bool isChanginging;

    public void ConnectToInput(ItemPoint innerPoint, ItemPoint outerPoint)
    {
        if (innerPoint != output)
        {
            Debug.LogError("Соединение не с той точкой");
            return;
        }
        publicOutput = outerPoint;
    }

    protected virtual void OnChangeStart()
    {

    }

    protected virtual void OnChangeEnd()
    {

    }

    protected virtual void OnItemMove()
    {

    }

    protected override void OnTick()
    {
        output.Move(publicOutput);

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
            if (center.Move(output))
            {
                OnItemMove();
            }
            isChanginging = input.Move(center);
            if (isChanginging)
            {
                OnChangeStart();
            }
        }
    }

    protected abstract Item GetNewItem(Item old);
}
