using UnityEngine;

public class Gear : Instrument, IResourse
{
    public bool IsBroken => brokenModel.activeSelf;
    [SerializeField] private GameObject brokenModel;
    [SerializeField] private GameObject repairedModel;
    [SerializeField] private GearPlace place;
    [SerializeField] private AudioSource breakSource;
    [SerializeField] private AudioSource placeSource;
    
    public void Break()
    {
        repairedModel.SetActive(false);
        brokenModel.SetActive(true);
        breakSource.Play();
    }

    public override void Use()
    {
        base.Use();
        Ray ray = new Ray(Interactor.instance.CameraTransform.position, Interactor.instance.CameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.rigidbody && hit.rigidbody.TryGetComponent<GearPlace>(out GearPlace p))
            {
                if (p.IsEmpty)
                {
                    place = p;
                    Interactor.instance.DropItem();
                    moveCor = StartCoroutine(GoToPoint(p.Point, () => { placeSource.Play(); }));
                    p.Place(this);
                }
            }
        }
    }

    public override void Interact()
    {
        if (PlayerInventory.instance.InHandItem == null)
        {
            if (place != null)
            {
                place.Take();
                place = null;
            }
        }
        base.Interact();
    }
}
