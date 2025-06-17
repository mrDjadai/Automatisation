using UnityEngine;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    [SerializeField]
    private string key;

    private LocalizationManager localizationManager => LocalizationManager.instance;
    private TMP_Text text;


    private void Awake()
    {
        if(text == null)
        {
            text = GetComponent<TMP_Text>();
        }
    }

    private void Start()
    {
        localizationManager.OnLanguageChanged += UpdateText;
        UpdateText();
    }

    private void OnDestroy()
    {
        if (localizationManager != null)
        {
            localizationManager.OnLanguageChanged -= UpdateText;
        }
    }

    virtual protected void UpdateText()
    {
        if (text == null)
        {
            text = GetComponent<TMP_Text>();
        }
        text.text = localizationManager.GetLocalizedValue(key);
    }
}