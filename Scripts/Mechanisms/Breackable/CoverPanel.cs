using UnityEngine;
using DG.Tweening;

public class CoverPanel : Interactable
{
    [SerializeField] private Transform panel;
    [SerializeField] private Transform openPoint;
    [SerializeField] private Transform closePoint;
    [SerializeField] private float openTime;
    [SerializeField] private CoverPanelBreackable breackable;
    [SerializeField] private AudioSource moveSource;
    private bool isOpen;
    private bool isAnimating;

    public override void EndInteract()
    {
    }

    public override void Interact()
    {
        if (isAnimating)
        {
            return;
        }

        isOpen = !isOpen;
        moveSource.Play();
        isAnimating = true;
        if (isOpen)
        {
            panel.DOMove(openPoint.position, openTime);
            panel.DORotateQuaternion(openPoint.rotation, openTime).OnComplete(() => { isAnimating = false; });
            breackable.Break();
        }
        else
        {
            panel.DOMove(closePoint.position, openTime);
            panel.DORotateQuaternion(closePoint.rotation, openTime).OnComplete(() => { isAnimating = false; });
            breackable.Repair();
        }
    }
}
