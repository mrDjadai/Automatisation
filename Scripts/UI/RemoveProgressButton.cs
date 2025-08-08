using UnityEngine;

[RequireComponent(typeof(CoverPanel))]
public class RemoveProgressButton : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    private CoverPanel panel;

    private void Awake()
    {
        panel = GetComponent<CoverPanel>();
    }

    private void OnMouseUpAsButton()
    {
        panel.Interact();
    }

    public void DeleteSave()
    {
        SaveManager.instance.DeleteSave();
        Destroy(SaveManager.instance.gameObject);
        sceneLoader.Restart();
    }
}
