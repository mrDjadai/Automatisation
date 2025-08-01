using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class ClickableButton : Interactable
{
    [SerializeField] private UnityEvent onPress;
    [SerializeField] private float animationTime;
    [SerializeField] private float animationOffset;

    private bool isPressed;

    public override void EndInteract()
    {
    }

    public override void Interact()
    {
        if (isPressed)
        {
            return;
        }
        isPressed = true;
        onPress.Invoke();
        transform.DOLocalMove(Vector3.up * animationOffset, animationTime / 2).OnComplete(() => {
            transform.DOLocalMove(Vector3.zero, animationTime / 2);
            isPressed = false;
        });
    }
}
