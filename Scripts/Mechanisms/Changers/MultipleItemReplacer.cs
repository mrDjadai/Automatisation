using UnityEngine;
using DG.Tweening;

public class MultipleItemReplacer : Tickable, IItemConnectable
{
    [SerializeField] private Item newItem;

    [SerializeField] private Point[] inputs;
    [SerializeField] private ItemPoint output;
    [SerializeField] private ItemPoint publicOutput;
    [SerializeField, Min(2)] private int tickToChange;


    [SerializeField] private int applyTick;
    public Transform[] outSimulated;

    [SerializeField] private float animationTime;
    [SerializeField] private AudioSource createSource;
    [SerializeField] protected Vector3 openAnle;

    private int tickAfterChange;
    private bool isChanginging;
    private bool changedMoved = true;

    public void ConnectToInput(ItemPoint innerPoint, ItemPoint outerPoint)
    {
        if (innerPoint != output)
        {
            Debug.LogError("Соединение не с той точкой");
            return;
        }
        publicOutput = outerPoint;
    }

    protected override void OnTick()
    {
        output.Move(publicOutput);


        if (isChanginging && changedMoved)
        {
            tickAfterChange++;
            if (tickAfterChange >= tickToChange)
            {
                isChanginging = false;
                changedMoved = false;
                tickAfterChange = 0;
                return;
            }
            if (tickAfterChange == applyTick)
            {
                Item item = inputs[0].center.Pop();
                Vector3 pos = item.transform.position;
                Quaternion rot = item.transform.rotation;
                Destroy(item.gameObject);

                for (int i = 1; i < inputs.Length; i++)
                {
                    Destroy(inputs[i].center.Pop().gameObject);
                }
                createSource.Play();
                GetNewItem(pos, rot).Move(inputs[0].center);
            }
        }
        else
        {

            if (changedMoved == false && inputs[0].center.IsEmpty == false)
            {
                changedMoved = inputs[0].center.Move(output);
                foreach (var item in outSimulated)
                {
                    item.DOLocalRotate(openAnle, 
                        animationTime / 2).OnComplete(() => { item.DOLocalRotate(Vector3.zero, animationTime / 2); });
                }
            }

            if (changedMoved)
            {
                for (int i = 0; i < inputs.Length; i++)
                {
                    if (inputs[i].input.Move(inputs[i].center))
                    {
                        foreach (var item in inputs[i].simulated)
                        {
                            item.DOLocalRotate((inputs[i].reverseAnimation ? -1 : 1) * openAnle,
                                animationTime / 2).OnComplete(() => { item.DOLocalRotate(Vector3.zero, animationTime / 2); });
                        }
                    }

                }
            }
            isChanginging = CheckCond();
        }
    }

    private bool CheckCond()
    {
        if (changedMoved == false)
        {
            return false;
        }
        foreach (var item in inputs)
        {
            if (item.center.IsEmpty)
            {
                return false;
            }
        }
        return true;
    }

     private Item GetNewItem(Vector3 pos, Quaternion rot)
     {
          Item nItem = Instantiate(newItem, pos, rot);
          nItem.Init(settings);
          return nItem;
     }

    [System.Serializable]
    private class Point
    {
        public ItemPoint center;
        public ItemPoint input;
        public Transform[] simulated;
        public bool reverseAnimation;
    }
}
