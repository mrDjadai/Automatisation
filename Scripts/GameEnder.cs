using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
public class GameEnder : MonoBehaviour
{
    [SerializeField] private float showTime;
    [SerializeField] private Image[] images;
    [SerializeField] private TextMeshProUGUI[] textes;
    [SerializeField] private PlayerController controller;
    [SerializeField] private Interactor interactor;
    [SerializeField] private Image centerPointer;
    [SerializeField] private GameObject[] winObjects;
    [SerializeField] private GameObject[] loseObjects;

    [SerializeField] private string rewardKey;
    [SerializeField] private TextMeshProUGUI rewardText;

    private bool isEnded;

    private int GetReward()
    {
        return 10;
    }

    private int GetMaxReward()
    {
        return 20;
    }

    public void Win()
    {
        if (isEnded)
        {
            return;
        }
        isEnded = true;

        int level = PlayerPrefs.GetInt("CurrentLevel");


        rewardText.gameObject.SetActive(true);
        rewardText.text = LocalizationManager.instance.GetLocalizedValue(rewardKey) + UpgradePointsTextSetter.GetText(GetReward())
            + '\\' + UpgradePointsTextSetter.GetText(GetMaxReward());

        if (SaveManager.instance.MaxLevel == level)
        {
            SaveManager.instance.UpgradePoints ++;
        }

        if (SaveManager.instance.MaxLevel == level)
        {
            SaveManager.instance.NextLevel();
        }
        foreach (var item in winObjects)
        {
            item.SetActive(true);
        }
        Show();
    }

    public void Lose()
    {
        if (isEnded)
        {
            return;
        }
        isEnded = true;
        foreach (var item in loseObjects)
        {
            item.SetActive(true);
        }
        Show();
    }

    public void Show()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        controller.enabled = false;
        interactor.enabled = false;

        centerPointer.DOColor(Color.clear, showTime);

        foreach (var item in textes)
        {
            Color c = item.color;
            c.a = 1;
            item.DOColor(c, showTime);
        }

        foreach (var item in images)
        {
            Color c = item.color;
            c.a = 1;
            item.DOColor(c, showTime);
        }
    }
}
