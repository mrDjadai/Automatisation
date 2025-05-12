using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class TutorialEnder : MonoBehaviour
{
    [SerializeField] private Breackable activator;
    [SerializeField] private float showTime;
    [SerializeField] private Image[] images;
    [SerializeField] private TextMeshProUGUI[] textes;
    [SerializeField] private PlayerController controller;
    [SerializeField] private Interactor interactor;
    [SerializeField] private Image centerPointer;

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

    private void Update()
    {
        if (activator.IsBroken == false)
        {
            enabled = false;
            if (PlayerPrefs.GetInt("Level") == 0)
            {
                PlayerPrefs.SetInt("Level", 1);
            }
            Show();
        }
    }
}
