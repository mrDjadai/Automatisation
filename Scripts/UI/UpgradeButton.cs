using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class UpgradeButton : MonoBehaviour
{
    public int Cost => cost;
    public string nameKey => upgradeName;
    public string descriptionKey => upgradeDescriptionKey;

    [SerializeField] private string upgradeName;
    [SerializeField] private string upgradeDescriptionKey;
    [SerializeField] private int cost;

    [SerializeField] private UpgradeButton[] nextButtons;
    [SerializeField] private UpgradeButton blockButton;
    [SerializeField] private bool activeOnStart;

    [SerializeField] private  Image arrow;
    [SerializeField] private  Image blocker;
    [SerializeField] private TMP_Text costText;
    private Button button;
    private Image buttonImage;


    [SerializeField] private Color unlockColor;
    [SerializeField] private Color activeColor;
    private Color inactiveColor;

    [SerializeField] private UpgradeUnlocker unlocker;


    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        button = GetComponent<Button>();
        inactiveColor = button.image.color;

        button.onClick.AddListener(OnClick);
        button.interactable = false;

        if (activeOnStart)
        {
            Activate();
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
        unlocker.SetUpgrade(this);
    }

    public void TryBuy()
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
        if (buttonImage.GetComponent<CanvasGroup>().alpha == 0)
        {
            Activate();
        }
        button.interactable = false;
        buttonImage.color = unlockColor;
        arrow.color = unlockColor;

        foreach (var item in nextButtons)
        {
            item.Activate();
        }

        if (blockButton != null)
        {
            blockButton.Block();
        }
    }

    public void Activate()
    {
        buttonImage.GetComponent<CanvasGroup>().alpha = 1;
        buttonImage.color = activeColor;
        button.interactable = true;
    }

    public void Block()
    {
        buttonImage.color = inactiveColor;
        button.interactable = false;
        blocker.gameObject.SetActive(true);
    }
}
