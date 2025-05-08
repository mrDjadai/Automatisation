using UnityEngine.UI;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public delegate void SimpleAction();
    public static Interactor instance { get; private set; }
    public Transform CameraTransform => cameraTransform;
    public LayerMask InteractLayers => interactLayers;
    public Vector3 LookPoint => lookPoint;
    public SimpleAction OnInteractionEnd = new SimpleAction(() => {});

    [SerializeField] private Image interactIndicator;
    [SerializeField] private Color defaultIndicatorColor;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float interactDistance;
    [SerializeField] private LayerMask interactLayers;
    [SerializeField] private LayerMask lookLayers;

    private IInteractable currentInteractable;
    private bool canInteract;
    private Vector3 lookPoint;

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
      /*  if (PlayerInventory.instance.InHandItem != null)
        {
            PlayerInventory.instance.DropItem();
            return;
        }*/
    }

    public void TryInteract()
    {
        if (canInteract)
        {
            if (currentInteractable != null)
            {
                currentInteractable.Interact();
            }
        }
    }

    public void InteractionEnd()
    {
        OnInteractionEnd.Invoke();
    }

    private void FixedUpdate()
    {
        Color indicatorColor = defaultIndicatorColor;
        canInteract = false;

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, interactLayers))
        {
            currentInteractable = raycastHit.transform.GetComponent<IInteractable>();
            if (currentInteractable != null)
            {
                if (raycastHit.distance <= interactDistance)
                {
                    indicatorColor = Color.white;
                    canInteract = true;
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
