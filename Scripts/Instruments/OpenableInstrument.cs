using UnityEngine;

public class OpenableInstrument : MonoBehaviour
{
    [SerializeField] private string openKey;

    private void Awake()
    {
        gameObject.SetActive(SaveManager.instance.HasUpgrade(openKey));
    }
}
