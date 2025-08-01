using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
public class GameEnder : MonoBehaviour
{
    [SerializeField] private int levelID;
    [SerializeField] private float showTime;
    [SerializeField] private Image[] images;
    [SerializeField] private TextMeshProUGUI[] textes;
    [SerializeField] private PlayerController controller;
    [SerializeField] private Interactor interactor;
    [SerializeField] private Image centerPointer;
    [SerializeField] private GameObject[] winObjects;
    [SerializeField] private GameObject[] loseObjects;

    private bool isEnded;

    public void Win()
    {
        if (isEnded)
        {
            return;
        }
        isEnded = true;
        if (PlayerPrefs.GetInt("Level") == levelID)
        {
            PlayerPrefs.SetInt("Level", levelID + 1);
            PlayerPrefs.SetInt("SkillPoint", PlayerPrefs.GetInt("SkillPoint") + 1);
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
