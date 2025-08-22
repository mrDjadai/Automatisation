using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance { get; private set; }

    public int MaxLevel => save.maxLevel;
    public string CharacterName => save.CharacterName;

    public int LastGazete
    {
        get
        {
            return save.lastGazete;
        }

        set
        {
            save.lastGazete = value;
            SaveGame();
        }
    }

    public int UpgradePoints
    {
        get
        {
            return save.upgradePoints;
        }
        set
        {
            save.upgradePoints = value;
            SaveGame();
        }
    }

    [SerializeField] private int levelCount;

    private SaveData save;

    public void NextLevel()
    {
        save.maxLevel++;
        SaveGame();
    }

    public bool HasUpgrade(string upgrade)
    {
        return save.unlockedUpgrades.Contains(upgrade);
    }

    public void UnlockUpgrade(string upgrade)
    {
        if (!save.unlockedUpgrades.Contains(upgrade))
        {
            save.unlockedUpgrades.Add(upgrade);
            SaveGame();
        }
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
            return;
        }

        Debug.Log("Save at " + GetSavePath());
        Debug.Log("Save file exists " + File.Exists(GetSavePath()).ToString());
        if (File.Exists(GetSavePath()))
        {
            LoadSave();
        }
        else
        {
            CreateEmptySave();
        }
    }

    public void DeleteSave()
    {
        File.Delete(GetSavePath());
    }

    private void CreateEmptySave()
    {
        save = new SaveData();

        save.unlockedUpgrades = new List<string>();

        GenerateRandomName();

        SaveGame();
    }

    private void LoadSave()
    {
        if (Debug.isDebugBuild)
        {
            save = JsonUtility.FromJson<SaveData>(File.ReadAllText(GetSavePath()));
        }
        else
        {
            save = JsonUtility.FromJson<SaveData>(Shifrator.Decrypt(File.ReadAllText(GetSavePath())));
        }
    }

    private string GetNamesPath()
    {
        return Application.streamingAssetsPath + "/languages/Names/" + "en_US" + ".json";
    }

    public void SaveGame()
    {
        if (Debug.isDebugBuild)
        {
            File.WriteAllText(GetSavePath(), JsonUtility.ToJson(save));
        }
        else
        {
            File.WriteAllText(GetSavePath(), Shifrator.Encrypt(JsonUtility.ToJson(save)));
        }
    }

    private string GetSavePath()
    {
        return Application.dataPath + "/Save.json";
    }


    private void Update()
    {
        if (Debug.isDebugBuild)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                UpgradePoints += 25;
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                NextLevel();
            }
        }
    }

    public void GenerateRandomName()
    {
        NamesData data = JsonUtility.FromJson<NamesData>(File.ReadAllText(GetNamesPath()));
        string[] names = data.names;
        string[] secondNames = data.secondNames;

        string n;
        do
        {
            n = names[Random.Range(0, names.Length)] + " " + secondNames[Random.Range(0, secondNames.Length)];
        } while (n == save.CharacterName);
        save.CharacterName = n;
        SaveGame();
    }
}

[System.Serializable]
public class NamesData
{
    public string[] names;
    public string[] secondNames;
}

[System.Serializable]
public class SaveData
{
    public int upgradePoints;

    public List<string> unlockedUpgrades;

    public int maxLevel;

    public int lastGazete;

    public string CharacterName;
}
