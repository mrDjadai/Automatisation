using UnityEngine;

public class ItemSeparator : Tickable
{
    [Header("В этом блоке")]
    [SerializeField] private ItemPoint[] outnputs;
    [SerializeField] private ItemPoint input;


    private int curOutput = 0;

    protected override void OnTick()
    {
        if (input.Move(outnputs[curOutput]))
        {
            curOutput = (curOutput + 1) % outnputs.Length;
        }
    }
}
