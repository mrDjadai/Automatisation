using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeUnlocker : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Button buyButton;

    private UpgradeButton currentButton;

    public void Hide()
    {
        currentButton = null;
        gameObject.SetActive(false);
    }

    public void SetUpgrade(UpgradeButton b)
    {
        currentButton = b;
        nameText.text = LocalizationManager.instance.GetLocalizedValue(b.nameKey);
        descriptionText.text = LocalizationManager.instance.GetLocalizedValue(b.descriptionKey);
        buyButton.interactable = (SaveManager.instance.UpgradePoints >= b.Cost);
        gameObject.SetActive(true);
    }

    public void Unlock()
    {
        if (currentButton != null)
        {
            currentButton.TryBuy();
        }
        Hide();
    }

    private void Start()
    {
        buyButton.onClick.AddListener(Unlock);
        gameObject.SetActive(false);
    }
}
