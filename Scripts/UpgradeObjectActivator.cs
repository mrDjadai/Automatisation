using UnityEngine;

public class UpgradeObjectActivator : MonoBehaviour
{
    [SerializeField] private string key;
    [SerializeField] private GameObject activatable;

    private void Start()
    {
        activatable.SetActive(SaveManager.instance.HasUpgrade(key));
    }
}
