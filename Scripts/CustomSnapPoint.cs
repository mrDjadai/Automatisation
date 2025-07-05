using UnityEngine;

public class CustomSnapPoint : MonoBehaviour
{
    [field : SerializeField] public bool IsOutput { get; private set; }
    [field : SerializeField] public ItemPoint Point { get; private set; }
    public IItemConnectable Connectable { get; private set; }

    [field: SerializeField] public GameObject ConnectableObject { get; private set; }

    private void OnDrawGizmos()
    {
        if (IsOutput)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawSphere(transform.position, 0.2f);
    }

    private void OnValidate()
    {
        if (ConnectableObject.TryGetComponent<IItemConnectable>(out IItemConnectable c))
        {
            Connectable = c;
        }
        else
        {
            ConnectableObject = null;
            Connectable = null;
        }
    }
}
