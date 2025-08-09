using UnityEngine;

public class ObjectTeleporter : MonoBehaviour
{
    [SerializeField] private Instrument movable;
    [SerializeField] private Transform target;

    public void Teleport()
    {
        if (PlayerInventory.instance.InHandItem != movable)
        {
            movable.transform.position = target.position;
        }
    }
}
