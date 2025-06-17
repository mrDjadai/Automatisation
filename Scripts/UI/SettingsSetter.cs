using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using UnityEngine;

public class SettingsSetter : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private TextMeshProUGUI soundText;
    [SerializeField, Range(0.0001f, 1)] private float defaultSoundVolume;
    [SerializeField, Range(0.0001f, 1)] private float defaultMusicVolume;
    [Header("Other")]
    [SerializeField] private Slider sensabilitySlider;
    [SerializeField] private TextMeshProUGUI sensabilityText;
    [SerializeField, Range(0.1f, 7.9f)] private float defaultSensability;
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private TextMeshProUGUI brightnessText;
    [SerializeField, Range(0, 100)] private float defaultBrightness;
    [SerializeField] private BrightnessSetter brightnessSetter;

    public void OnMusicSliderMove()
    {
        SetMusicVolume(musicSlider.value);
    }

    public void OnSoundSliderMove()
    {
        SetSoundVolume(soundSlider.value);
    }

    public void OnSensabilitySliderMove()
    {
        float sensability = sensabilitySlider.value;
        sensabilityText.text = Mathf.RoundToInt((sensability - 0.1f) / 7.8f * 100).ToString() + "%";
        PlayerPrefs.SetFloat("Sensability", sensability);
    }

    public void OnBrightnessSliderMove()
    {
        float brightness = brightnessSlider.value;
        brightnessText.text = Mathf.RoundToInt(brightness).ToString() + "%";
        PlayerPrefs.SetFloat("Brightness", brightness);
        brightnessSetter.SetBrightness();
    }

    private void Awake()
    {
        if (PlayerPrefs.HasKey("SetDefaultSettings") == false)
        {
            SetDefaultSettings();
            PlayerPrefs.SetString("SetDefaultSettings", "1");
        }
    }

    private void Start()
    {
        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume");
        SetSoundVolume(soundSlider.value);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SetMusicVolume(musicSlider.value);

        sensabilitySlider.value = PlayerPrefs.GetFloat("Sensability");
        sensabilityText.text = Mathf.CeilToInt((sensabilitySlider.value - 0.1f) / 7.8f * 100).ToString() + "%";
        brightnessSlider.value = PlayerPrefs.GetFloat("Brightness");
    }

    private void SetDefaultSettings()
    {
        PlayerPrefs.SetFloat("MusicVolume", defaultMusicVolume);
        PlayerPrefs.SetFloat("SoundVolume", defaultSoundVolume);
        PlayerPrefs.SetFloat("Sensability", defaultSensability);
        PlayerPrefs.SetFloat("Brightness", defaultBrightness);
        PlayerPrefs.SetInt("FullScreen", 1);
    }

    private void SetMusicVolume(float volume)
    {
        float musicVolume = Mathf.Clamp(volume, 0.0001f, 1);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
        musicText.text = Mathf.RoundToInt(musicVolume * 100).ToString() + "%";
    }

    private void SetSoundVolume(float volume)
    {
        float soundVolume = Mathf.Clamp(volume, 0.0001f, 1);
        PlayerPrefs.SetFloat("SoundVolume", soundVolume);
        audioMixer.SetFloat("SoundVolume", Mathf.Log10(soundVolume) * 20);
        soundText.text = Mathf.RoundToInt(soundVolume * 100).ToString() + "%";
    }
}
