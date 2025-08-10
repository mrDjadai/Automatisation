using UnityEngine;

public abstract class Interactable : MonoBehaviour, ILookDetectable
{
    public bool CanUnfocus => canUnfocus;
    [SerializeField] private MeshRenderer[] outlineRenderers;
    [SerializeField] private bool canUnfocus = true;

    protected virtual void Awake()
    {
        foreach (var item in outlineRenderers)
        {
            item.enabled = false;
            item.gameObject.SetActive(true);
        }
    }

    public abstract void Interact();

    public abstract void EndInteract();

    public void SetOutline(bool v)
    {
        foreach (var item in outlineRenderers)
        {
            item.enabled = v;
        }
    }

    public virtual void OnStartLook() 
    {
    }

    public virtual void OnEndLook()
    {
    }
}

public interface ILookDetectable
{
    public void OnStartLook();
    public void OnEndLook();
    public void Interact();
    public void EndInteract();
}