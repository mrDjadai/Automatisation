using UnityEngine;

public class Text3D : MonoBehaviour
{
    [SerializeField] private int copyCount;
    [SerializeField] private float offset;

    private void Start()
    {
        Transform tr = transform;
        for (int i = 1; i <= copyCount; i++)
        {
            GameObject cur = Instantiate(gameObject, tr.parent);
            cur.transform.parent = tr;
            Destroy(cur.GetComponent<Text3D>());
            cur.transform.localPosition = new Vector3(0, 0, -offset * i);
        }
    }
}
