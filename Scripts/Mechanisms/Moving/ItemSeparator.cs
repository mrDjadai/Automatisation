using UnityEngine;
using DG.Tweening;

public class ItemSeparator : Tickable, IItemConnectable
{
    [Header("В этом блоке")]
    [SerializeField] private ItemPoint[] outnputs;
    [SerializeField] private ItemPoint input;
    [SerializeField] private ItemPoint[] publicOutputs;
    [SerializeField] private Transform rotatable;
    [SerializeField] private float[] angles;
    [SerializeField] private float animationTime;

    private int curOutput = 0;

    public void ConnectToInput(ItemPoint innerPoint, ItemPoint outerPoint)
    {
        for (int i = 0; i < outnputs.Length; i++)
        {
            if (outnputs[i] == innerPoint)
            {
                publicOutputs[i] = outerPoint;
                return;
            }
        }
        Debug.LogError("Соединение не с той точкой");
    }

    private void OnValidate()
    {
        if (outnputs.Length != publicOutputs.Length)
        {
            ItemPoint[] temp = new ItemPoint[outnputs.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = publicOutputs[i];
            }
            publicOutputs = temp;
        }
    }

    protected override void OnTick()
    {
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
