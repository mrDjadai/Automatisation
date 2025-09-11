using UnityEngine;
using DG.Tweening;

public class ConveerBorder : MonoBehaviour
{
    [SerializeField] private Transform[] simulated;
    [SerializeField] private float animationTime;
    [SerializeField] private float animationDelay;
    [SerializeField] protected Vector3 openAnle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody && other.attachedRigidbody.GetComponent<Item>())
        {
            Animate();
        }
    }

    private void Animate()
    {
        foreach (var item in simulated)
        {
            item.DOLocalRotate(openAnle, animationTime / 2).OnComplete(() => { item.DOLocalRotate(Vector3.zero, animationTime / 2); }).SetDelay(animationDelay);
        }
    }
}
