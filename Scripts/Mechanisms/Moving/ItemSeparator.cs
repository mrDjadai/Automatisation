using UnityEngine;

public class ItemSeparator : Tickable
{
    [Header("В этом блоке")]
    [SerializeField] private ItemPoint[] outnputs;
    [SerializeField] private ItemPoint input;
    [SerializeField] private ItemPoint publicInput;
    [SerializeField] private ItemPoint[] publicOutputs;


    private int curOutput = 0;

    protected override void OnTick()
    {
        if (publicInput != null)
        {
            publicInput.Move(input);
        }
        if (input.Move(outnputs[curOutput]))
        {
            curOutput = (curOutput + 1) % outnputs.Length;
        }
        for (int i = 0; i < outnputs.Length; i++)
        {
            if (publicOutputs[i] != null)
            {
                outnputs[i].Move(publicOutputs[i]);
            }
        }

    }
}
