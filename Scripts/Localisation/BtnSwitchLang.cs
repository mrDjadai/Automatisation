using UnityEngine;

public class BtnSwitchLang: MonoBehaviour
{
    [SerializeField] private bool useAlternativeFont;
    [SerializeField] private Light indicator;

    private void SetIndicator()
    {
        indicator.enabled = LocalizationManager.instance.CurrentLanguage == name;
    }

    private void Start()
    {
        LocalizationManager.instance.OnLanguageChanged += SetIndicator;
        SetIndicator();
    }

    private void OnDestroy()
    {
        if (LocalizationManager.instance != null)
        {
            LocalizationManager.instance.OnLanguageChanged -= SetIndicator;
        }
    }

    public void OnButtonClick()
    {
        LocalizationManager.instance.IsAlterntativeFont = useAlternativeFont;
		LocalizationManager.instance.CurrentLanguage = name;
    }
}