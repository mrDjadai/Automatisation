using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class PhysicalButton : MonoBehaviour
{
    [SerializeField] private UnityEvent onClick;
    [SerializeField] private Transform movable;
    [SerializeField] private Vector3 targetLocalPos;
    [SerializeField] private float animationTime;
    private bool isMoving;

    private void OnMouseUpAsButton()
    {
        if (isMoving)
        {
            return;
        }

        isMoving = true;
        movable.DOLocalMove(targetLocalPos, animationTime / 2).OnComplete(OnClick);
    }

    private void OnClick()
    {
        movable.DOLocalMove(Vector3.zero, animationTime / 2).OnComplete(() => { isMoving = false; });
        onClick.Invoke();
    }
}
