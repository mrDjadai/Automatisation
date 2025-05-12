using UnityEngine;
using DG.Tweening;

public class ItemSeparator : Tickable
{
    [Header("В этом блоке")]
    [SerializeField] private ItemPoint[] outnputs;
    [SerializeField] private ItemPoint input;
    [SerializeField] private ItemPoint publicInput;
    [SerializeField] private ItemPoint[] publicOutputs;
    [SerializeField] private Transform rotatable;
    [SerializeField] private float[] angles;
    [SerializeField] private float animationTime;

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
            rotatable.DOLocalRotate(Vector3.up * angles[curOutput], animationTime);
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
