using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class InstrumentOpener : MonoBehaviour
{
    [SerializeField] private string upgradeName;
    [SerializeField] private int cost;

    [SerializeField] private TMP_Text costText;
    [SerializeField] private GameObject upgradeObject;
    [SerializeField] private float unlockTime;
    [SerializeField] private Transform model;

    private MeshRenderer[] renderers;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(OnClick);
        renderers = model.GetComponentsInChildren<MeshRenderer>();
        foreach (var item in renderers)
        {
            item.material.SetFloat("_Saturation", 0);
        }
    }

    private void Start()
    {
        if (SaveManager.instance.HasUpgrade(upgradeName))
        {
            Unlock();
        }
    }

    private void OnClick()
    {
        if (SaveManager.instance.UpgradePoints >= cost)
        {
            SaveManager.instance.UpgradePoints -= cost;
            SaveManager.instance.UnlockUpgrade(upgradeName);

            Unlock();
        }
    }

    public void Unlock()
    {
        StartCoroutine(Show());
        button.image.raycastTarget = false;
        button.image.enabled = false;
        button.interactable = false;
        foreach (var item in button.transform.GetComponentsInChildren<Transform>())
        {
            item.gameObject.SetActive(false);
        }

        upgradeObject.SetActive(true);
    }

    private IEnumerator Show()
    {
        float t = 0;
        while (t < unlockTime)
        {
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime;
            float v = t / unlockTime;

            foreach (var item in renderers)
            {
                item.material.SetFloat("_Saturation", v);
            }
        }
    }
}
