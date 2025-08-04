using UnityEngine;

public class GearHolder : Instrument
{
    [SerializeField] private Gear[] gears;

    public override void Use()
    {
        for (int i = 0; i < gears.Length; i++)
        {
            if (gears[i] != null)
            {
                gears[i].Use();

                if (gears[i].IsPlaced)
                {
                    gears[i].GetComponent<Rigidbody>().excludeLayers = 0;
                    gears[i] = null;
                }
                break;
            }
        }

        foreach (var item in gears)
        {
            if (item == null)
            {
                return;
            }
        }

        Interactor.instance.DropItem();
        Destroy(gameObject);
    }
}
