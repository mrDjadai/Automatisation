using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButtonCreator : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private int firstLevelScene;
    [SerializeField] private int levelCount;
    [SerializeField] private Button buttonPrefab;
    [SerializeField] private Transform origin;

    private void Awake()
    {
        int unclocked = PlayerPrefs.GetInt("Level");

        for (int i = 0; i < levelCount; i++)
        {
            int num = i;
            Button b = Instantiate(buttonPrefab, origin);
            b.interactable = i <= unclocked;
            b.onClick.AddListener(() => { sceneLoader.LoadScene(firstLevelScene + num); });
            b.GetComponentInChildren<TextMeshProUGUI>().text = (i + 1).ToString();
        }
    }
}
