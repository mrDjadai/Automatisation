using UnityEngine.UI;
using UnityEngine;

public class FontLocalisator : MonoBehaviour
{
    private const int maxAlternativeSize = 110;
    private int fontSize;

    private LocalizationManager localizationManager;
    private Text text;


    private void Awake()
    {
        if (localizationManager == null)
        {
            localizationManager = GameObject.FindGameObjectWithTag("LocalizationManager").GetComponent<LocalizationManager>();
        }
        if (text == null)
        {
            text = GetComponent<Text>();
        }
        localizationManager.OnLanguageChanged += UpdateText;
        fontSize = text.fontSize;
    }

    private void Start()
    {
        UpdateText();
    }

    private void OnDestroy()
    {
        localizationManager.OnLanguageChanged -= UpdateText;
    }

    virtual protected void UpdateText()
    {
        if (gameObject == null) return;

        if (localizationManager == null)
        {
            localizationManager = LocalizationManager.instance;
        }
        if (text == null)
        {
            text = GetComponent<Text>();
            fontSize = text.fontSize;
        }
        text.font = localizationManager.GetCurrentFont();

        if (localizationManager.IsAlterntativeFont)
        {
            text.fontSize = (text.fontSize >= maxAlternativeSize) ? maxAlternativeSize : fontSize;
        }
        else
        {
            text.fontSize = fontSize;
        }
    }
}
