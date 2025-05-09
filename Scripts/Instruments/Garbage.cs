using UnityEngine;

public class Garbage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody && other.attachedRigidbody.GetComponent<IResourse>() != null)
        {
            if (other.attachedRigidbody.isKinematic == false)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
