using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private string upgradeName;
    [SerializeField] private int cost;

    [SerializeField] private UpgradeButton[] nextButtons;
    [SerializeField] private bool activeOnStart;

    [SerializeField] private  Image arrow;
    [SerializeField] private TMP_Text costText;
    private Button button;
    private Image buttonImage;


    [SerializeField] private Color unlockColor;
    [SerializeField] private Color activeColor;

    private bool isUnlocked;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        button = GetComponent<Button>();

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
        if (SaveManager.instance.UpgradePoints >= cost)
        {
            SaveManager.instance.UpgradePoints -= cost;
            SaveManager.instance.UnlockUpgrade(upgradeName);

            Unlock();
        }
    }

    public void Unlock()
    {
        button.interactable = false;
        buttonImage.color = unlockColor;
        arrow.color = unlockColor;

        foreach (var item in nextButtons)
        {
            item.Activate();
        }

        isUnlocked = true;
    }

    public void Activate()
    {
        if (isUnlocked)
        {
            return;
        }

        buttonImage.color = activeColor;
        button.interactable = true;
    }
}
