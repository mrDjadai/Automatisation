using UnityEngine.UI;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public delegate void SimpleAction();
    public static Interactor instance { get; private set; }
    public Transform CameraTransform => cameraTransform;
    public LayerMask InteractLayers => interactLayers;
    public Vector3 LookPoint => lookPoint;

    [SerializeField] private Image interactIndicator;
    [SerializeField] private Color defaultIndicatorColor;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float interactDistance;
    [SerializeField] private LayerMask interactLayers;
    [SerializeField] private LayerMask lookLayers;

    private Interactable currentInteractable;
    private ILookDetectable currentLookDetectable;
    private bool canInteract;
    private Vector3 lookPoint;
    private bool interacting;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            throw new System.Exception("Может существовать только 1 экземпляр Interactor");
        }
    }

    public void DropItem()
    {
        if (PlayerInventory.instance.InHandItem != null)
        {
            PlayerInventory.instance.DropItem();
            return;
        }
    }

    public void TryInteract()
    {
        if (canInteract)
        {
            if (currentLookDetectable != null)
            {
                interacting = true;
                currentLookDetectable.Interact();
            }
        }
    }

    public void InteractionEnd()
    {
        if (currentLookDetectable != null)
        {
            interacting = false;
            currentLookDetectable.EndInteract();
        }
    }

    private void FixedUpdate()
    {
        Color indicatorColor = defaultIndicatorColor;
        canInteract = false;

        if (interacting && currentInteractable.CanUnfocus == false)
        {
            return;
        }

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, interactLayers))
        {
            Interactable newInteractable;
            newInteractable = raycastHit.transform.GetComponent<Interactable>();
            ILookDetectable newLookDetectable = raycastHit.transform.GetComponent<ILookDetectable>();

            if (currentInteractable != newInteractable)
            {
                InteractionEnd();
                if (currentInteractable != null)
                {
                    currentInteractable.SetOutline(false);
                }
            }

            if (currentLookDetectable != newLookDetectable)
            {
                if (currentLookDetectable != null)
                {
                    currentLookDetectable.OnEndLook();
                }
                if (newLookDetectable != null)
                {
                    newLookDetectable.OnStartLook();
                }
            }

            currentInteractable = newInteractable;
            currentLookDetectable = newLookDetectable;

            if (currentInteractable != null)
            {
                currentInteractable.SetOutline(true);
            }
            if (currentInteractable != null)
            {
                if (raycastHit.distance <= interactDistance)
                {
                    indicatorColor = Color.white;
                    canInteract = true;
                }
            }
            else
            {
                InteractionEnd();
                if (currentInteractable != null)
                {
                    currentInteractable.SetOutline(false);
                }
            }
        }
        interactIndicator.color = indicatorColor;

        Ray ray1 = new Ray(cameraTransform.position, cameraTransform.forward);
        if (Physics.Raycast(ray1, out RaycastHit hit, Mathf.Infinity, lookLayers))
        {
            lookPoint = hit.point;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(lookPoint, 0.1f);
    }
}
