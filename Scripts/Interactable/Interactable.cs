using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool CanUnfocus => canUnfocus;
    [SerializeField] private MeshRenderer[] outlineRenderers;
    [SerializeField] private bool canUnfocus = true;

    public abstract void Interact();

    public abstract void EndInteract();

    public void SetOutline(bool v)
    {
        foreach (var item in outlineRenderers)
        {
            item.enabled = v;
        }
    }
}
