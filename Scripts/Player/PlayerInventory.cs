using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public float ItemMovingSpeed => itemMovingSpeed;
    public float ItemRotatingSpeed => itemRotatingSpeed;
    public Transform HandPoint => handPoint;
    public Instrument InHandItem => inHandItem;
    public static PlayerInventory instance;

    [SerializeField] private CharacterController player;
    [SerializeField] private Transform handPoint;
    [SerializeField] private int inHandLayer;

    [SerializeField] private float itemMovingSpeed;
    [SerializeField] private float itemRotatingSpeed;

    private Instrument inHandItem;

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

    public void SetInHandItem(Instrument item)
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
            OnDropItem(inHandItem);
            inHandItem = null;
        }
    }

    public void OnDropItem(Instrument i)
    {
        i.Drop();
        SetObjectLayer(i.transform, i.DefaultLayer);

        i.GetComponent<Rigidbody>().linearVelocity = player.velocity;
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
    }
}
