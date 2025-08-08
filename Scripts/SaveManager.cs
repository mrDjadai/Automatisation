using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance { get; private set; }

    public int MaxLevel => save.maxLevel;

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

    public int GetCurrentLevelReward(int num)
    {
        return save.levelTakenRewards[num - 1];
    }   

    public void SetCurrentLevelReward(int num, int value)
    {
        save.levelTakenRewards[num - 1] = value;
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

        save.levelTakenRewards = new int[levelCount];

        save.unlockedUpgrades = new List<string>();

        SaveGame();
    }

    private void LoadSave()
    {
        save = JsonUtility.FromJson<SaveData>(Shifrator.Decrypt(File.ReadAllText(GetSavePath())));
    }

    public void SaveGame()
    {
        File.WriteAllText(GetSavePath(), Shifrator.Encrypt(JsonUtility.ToJson(save)));
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
}

[System.Serializable]
public class SaveData
{
    public int upgradePoints;

    public int[] levelTakenRewards;

    public List<string> unlockedUpgrades;

    public int maxLevel;
}
