using UnityEngine.UI;
using UnityEngine;

public class BossAngerIndicator : PercentIndicator
{
    [SerializeField] private Slider slider;
    [SerializeField] private Light lamp;
    [SerializeField] private MeshRenderer lampRenderer;
    [SerializeField] private AngerLevel[] levels;
    [SerializeField] private TMPro.TMP_Text percent;

    public override void SetValue(float v)
    {
        slider.SetValueWithoutNotify(v);
        slider.onValueChanged.Invoke(v);

        for (int i = levels.Length - 1; i >= 0; i--)
        {
            if (v >= levels[i].minValue)
            {
                SetColor(levels[i].color);
                return;
            }
        }
        percent.text = Mathf.RoundToInt(v * 100).ToString();
    }

    private void SetColor(Color c)
    {
        lamp.color = c;
        lampRenderer.material.SetColor("_BaseColor", c);
        lampRenderer.material.SetColor("_EmissionColor", c);
    }

    [System.Serializable]
    private struct AngerLevel
    {
        [Range(0, 1)] public float minValue;
        public Color color;
    }
}
