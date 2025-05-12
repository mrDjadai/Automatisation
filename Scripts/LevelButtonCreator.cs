using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButtonCreator : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private int firstLevelScene;
    [SerializeField] private int firstLevelDay;
    [SerializeField] private int levelCount;
    [SerializeField] private Button buttonPrefab;
    [SerializeField] private Transform origin;
    [SerializeField] private Color holidayColor;

    private void Awake()
    {
        int unclocked = PlayerPrefs.GetInt("Level");

        for (int i = 1; i < 32; i++)
        {
            Button b = Instantiate(buttonPrefab, origin);
            b.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = i.ToString();

            if (i % 7 == 0 || i % 7 == 6)
            {
                b.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = holidayColor;
            }

            if (i >= firstLevelDay && i < firstLevelDay + levelCount)
            {
                int num = i - firstLevelDay;
                b.interactable = num <= unclocked;
                b.onClick.AddListener(() => { sceneLoader.LoadScene(firstLevelScene + num); });
                b.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "смена " + (num + 1).ToString();
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

            if (i >= firstLevelDay && i < firstLevelDay + levelCount)
            {
                b.transform.GetChild(4).gameObject.SetActive(true);
            }
        }
    }
}
