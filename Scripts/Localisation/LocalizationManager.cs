using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class LocalizationManager : MonoBehaviour
{
    [HideInInspector] public bool IsAlterntativeFont = false;
    public static LocalizationManager instance { get; private set; }
    [SerializeField] private Font baseFont;
    [SerializeField] private Font alternativeFont;

    private string currentLanguage;
    private Dictionary<string, string> localizedText;
    public static bool isReady = false;

	public delegate void ChangeLangText();
    public event ChangeLangText OnLanguageChanged;

    public Font GetCurrentFont()
    {
        if (!IsAlterntativeFont)
        {
            return baseFont;
        }
        return alternativeFont;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Init();
    }

    public void Init()
    {
        StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        yield return new WaitForEndOfFrame();
  //      if (SteamManager.Initialized)
        if(false)
        {
       /*     Debug.Log(SteamUtils.GetSteamUILanguage());
            if (!PlayerPrefs.HasKey("Language"))
            {
                switch (SteamUtils.GetSteamUILanguage())
                {
                    case "russian":
                        PlayerPrefs.SetString("Language", "ru_RU");
                        break;
                    case "ukrainian":
                        PlayerPrefs.SetString("Language", "ru_RU");
                        break;
                    case "french":
                        PlayerPrefs.SetString("Language", "fr_FR");
                        break;
                    case "portuguese":
                        PlayerPrefs.SetString("Language", "pt_PT");
                        break;
                    case "spanish":
                        PlayerPrefs.SetString("Language", "es_ES");
                        break;
                    case "german":
                        PlayerPrefs.SetString("Language", "de_CH");
                        break;
                    case "italian":
                        PlayerPrefs.SetString("Language", "it_IT");
                        break;
                    case "schinese":
                        PlayerPrefs.SetString("Language", "zh-CHS");
                        break;
                    default:
                        PlayerPrefs.SetString("Language", "en_US");
                        break;
                }

            }*/
        }
        else
        {
            if (!PlayerPrefs.HasKey("Language"))
            {
                PlayerPrefs.SetString("Language", "en_US");
            }
        }

        Debug.Log("SetDefaultLanguage");
        currentLanguage = PlayerPrefs.GetString("Language");
        IsAlterntativeFont = currentLanguage == "zh-CHS";

        Debug.Log("GetLanguagePrefs");
        LoadLocalizedText(currentLanguage);
    }

    public void LoadLocalizedText(string langName)
    {
        Debug.Log("StartedLoading");
        string path = Application.streamingAssetsPath + "/languages/" + langName + ".json";
        Debug.Log("GotPath");
        string dataAsJson;

        dataAsJson = File.ReadAllText(path);

        Debug.Log("EndReading");
        Debug.Log(dataAsJson.Length);
        Debug.Log(dataAsJson);
        LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

        Debug.Log("GotData " + loadedData.items.Length.ToString());
        localizedText = new Dictionary<string, string>();
        for (int i = 0; i < loadedData.items.Length; i++)
        {
            localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
        }
        Debug.Log("SetDictionary");

        PlayerPrefs.SetString("Language", langName);
        currentLanguage = PlayerPrefs.GetString("Language");
        Debug.Log("SetCurrentLanguage");
        isReady = true;

        OnLanguageChanged?.Invoke();
        Debug.Log("InvoleDelegate");
    }


    public string GetLocalizedValue(string key)
    {
        if (localizedText != null && localizedText.ContainsKey(key))
        {
            return localizedText[key];
        }
        else
        {
            Debug.LogWarning("Localized text with key \"" + key + "\" not found");
            return key;
        }
    }

    public string CurrentLanguage
    {
        get 
        {
            return currentLanguage;
        }
        set
        {
			LoadLocalizedText(value);			
        }
    }
    public bool IsReady
    {
        get
        {
            return isReady;
        }
    }
}