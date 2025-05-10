using UnityEngine;

public class MultipleItemReplacer : Tickable
{
    [SerializeField] private Item newItem;

    [SerializeField] private ItemPoint[] inputs;
    [SerializeField] private ItemPoint[] centers;
    [SerializeField] private ItemPoint output;
    [SerializeField, Min(2)] private int tickToChange;
    [SerializeField] private int applyTick;
    private int tickAfterChange;
    private bool isChanginging;
    private bool changedMoved = true;

    protected override void OnTick()
    {
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
                Item item = centers[0].Pop();
                Vector3 pos = item.transform.position;
                Quaternion rot = item.transform.rotation;
                Destroy(item.gameObject);

                for (int i = 1; i < centers.Length; i++)
                {
                    Destroy(centers[i].Pop().gameObject);
                }
                GetNewItem(pos, rot).Move(centers[0]);
            }
        }
        else
        {

            if (changedMoved == false && centers[0].IsEmpty == false)
            {
                changedMoved = centers[0].Move(output);
            }

            if (changedMoved)
            {
                for (int i = 0; i < centers.Length; i++)
                {
                    inputs[i].Move(centers[i]);
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
        foreach (var item in centers)
        {
            if (item.IsEmpty)
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
}
