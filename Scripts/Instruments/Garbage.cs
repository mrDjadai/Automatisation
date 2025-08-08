using UnityEngine;

public class Garbage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent<IResourse>(out IResourse res))
        {
            if (other.attachedRigidbody.isKinematic == false)
            {
                res.OnGarbageDestroy();
                Destroy(other.gameObject);
            }
        }
    }
}
