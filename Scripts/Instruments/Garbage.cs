using UnityEngine;
using DG.Tweening;

public class Garbage : MonoBehaviour
{
    [SerializeField] private float destroyTime = 0.4f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent<IResourse>(out IResourse res))
        {
            if (other.attachedRigidbody.isKinematic == false)
            {
                res.OnGarbageDestroy();

                other.attachedRigidbody.transform.DOScale(Vector3.zero, destroyTime).OnComplete(() =>
                {
                    Destroy(other.gameObject);
                });
            }
        }
    }
}
