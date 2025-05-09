using UnityEngine;
using DG.Tweening;

public class CoverPanel : Interactable
{
    [SerializeField] private Transform panel;
    [SerializeField] private Transform openPoint;
    [SerializeField] private Transform closePoint;
    [SerializeField] private float openTime;
    [SerializeField] private CoverPanelBreackable breackable;
    private bool isOpen;

    public override void EndInteract()
    {
    }

    public override void Interact()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            panel.DOMove(openPoint.position, openTime);
            panel.DORotateQuaternion(openPoint.rotation, openTime);
            breackable.Repair();
        }
        else
        {
            panel.DOMove(closePoint.position, openTime);
            panel.DORotateQuaternion(closePoint.rotation, openTime);
            breackable.Break();
        }
    }
}
