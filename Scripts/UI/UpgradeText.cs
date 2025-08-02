using UnityEngine;
using TMPro;

public class UpgradeText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private string key;

    private void Update()
    {
        text.text = LocalizationManager.instance.GetLocalizedValue(key)
            + UpgradePointsTextSetter.GetText(SaveManager.instance.UpgradePoints);
    }
}
