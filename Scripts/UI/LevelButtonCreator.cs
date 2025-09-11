using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class LevelButtonCreator : MonoBehaviour
{
    public bool canOpen { get; set; }
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private int[] levelScenes;

    [SerializeField] private int firstLevelDay;
    [SerializeField] private Button buttonPrefab;
    [SerializeField] private Transform origin;
    [SerializeField] private Color holidayColor;

    private List<TextMeshProUGUI> shiftes = new List<TextMeshProUGUI>();

    private void Start()
    {
        LocalizationManager.instance.OnLanguageChanged += Localisate;
        int unclocked = SaveManager.instance.MaxLevel;

        for (int i = 1; i < 32; i++)
        {
            Button b = Instantiate(buttonPrefab, origin);
            b.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = i.ToString();

            if (i % 7 == 0 || i % 7 == 6)
            {
                b.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = holidayColor;
            }

            if (i >= firstLevelDay && i < firstLevelDay + levelScenes.Length)
            {
                int num = i - firstLevelDay;
                b.interactable = num <= unclocked;
                b.onClick.AddListener(() => { OpenLevel(num); });
                shiftes.Add(b.transform.GetChild(0).GetComponent<TextMeshProUGUI>());
            }
            else
            {
                b.interactable = false;
                b.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
            }

            if (i < firstLevelDay + unclocked)
            {
                b.transform.GetChild(2).gameObject.SetActive(true);
            }

            if (i == firstLevelDay + unclocked)
            {
                b.transform.GetChild(3).gameObject.SetActive(true);
            }

            if (i >= firstLevelDay && i < firstLevelDay + levelScenes.Length)
            {
                b.transform.GetChild(4).gameObject.SetActive(true);
            }
        }

        Localisate();
    }

    private void OnDestroy()
    {
        LocalizationManager.instance.OnLanguageChanged -= Localisate;
    }

    private void Localisate()
    {
        for (int i = 0; i < shiftes.Count; i++)
        {
            shiftes[i].text = LocalizationManager.instance.GetLocalizedValue("shift")
                    + (i + 1).ToString();
        }
    }

    private void OpenLevel(int num)
    {
        if (!canOpen)
        {
            return;
        }
        PlayerPrefs.SetInt("CurrentLevel", num);
        sceneLoader.LoadScene(levelScenes[num]);
    }
}
