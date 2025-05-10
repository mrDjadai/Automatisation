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

    public override void EndInteract()
    {
    }

    public override void Interact()
    {
        isOpen = !isOpen;
        moveSource.Play();
        if (isOpen)
        {
            panel.DOMove(openPoint.position, openTime);
            panel.DORotateQuaternion(openPoint.rotation, openTime);
            breackable.Break();
        }
        else
        {
            panel.DOMove(closePoint.position, openTime);
            panel.DORotateQuaternion(closePoint.rotation, openTime);
            breackable.Repair();
        }
    }
}
