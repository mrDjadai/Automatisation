using UnityEngine;

public class Gear : Instrument, IResourse
{
    public bool IsBroken => brokenModel.activeSelf;
    public bool IsPlaced => place != null;
    [SerializeField] private GameObject brokenModel;
    [SerializeField] private GameObject repairedModel;
    [SerializeField] private GearPlace place;
    [SerializeField] private AudioSource breakSource;
    [SerializeField] private AudioSource placeSource;
    [SerializeField] private float jumpForce;
    [SerializeField] private string jumpKey;

    public void Break()
    {
        repairedModel.SetActive(false);
        brokenModel.SetActive(true);
        breakSource.Play();

        if (SaveManager.instance.HasUpgrade(jumpKey))
        {
            place.Take();
            place = null;
            rb.isKinematic = false;
            rb.AddForce(transform.forward * jumpForce, ForceMode.Impulse);
        }
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

                    if (PlayerInventory.instance.InHandItem == this)
                    {
                        Interactor.instance.DropItem();
                    }
                    else
                    {
                        PlayerInventory.instance.OnDropItem(this);
                    }
                    moveCor = StartCoroutine(GoToPoint(p.Point, () => { placeSource.Play(); }));
                    p.Place(this);
                }
            }
        }
    }

    public override void Interact()
    {
        if (place != null)
        {
            place.Take();
            place = null;
        }
        base.Interact();
    }
}
