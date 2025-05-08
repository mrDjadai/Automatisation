using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Transform HandPoint => handPoint;
   // public Item InHandItem => inHandItem;
    public static PlayerInventory instance;

    [SerializeField] private CharacterController player;
    [SerializeField] private Transform handPoint;
    [SerializeField] private int inHandLayer;

    //private Item inHandItem;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            throw new System.Exception("����� ���� ������ 1 ��������� PlayerData");
        }
    }

   /* public void SetInHandItem(Item item)
    {
        if (inHandItem != null)
        {
            DropItem();
        }

        if (item != null)
        {
            inHandItem = item;
            SetObjectLayer(inHandItem.transform, inHandLayer);
        }
    }

    public void DropItem()
    {
        if (inHandItem != null)
        {
            inHandItem.Drop();
            SetObjectLayer(inHandItem.transform, inHandItem.DefaultLayer);

            inHandItem.GetComponent<Rigidbody>().linearVelocity = player.velocity;
            inHandItem = null;
        }
    }

    public void UseItem()
    {
        if (inHandItem != null)
        {
            inHandItem.Use();
        }
    }

    private void SetObjectLayer(Transform tr, int layer)
    {
        tr.gameObject.layer = layer;
        for (int i = 0; i < tr.childCount; i++)
        {
            tr.GetChild(i).gameObject.layer = layer;
            SetObjectLayer(tr.GetChild(i), layer);
        }
    }*/
}
