using UnityEngine;
using Zenject;
using TMPro;

public class ItemsManager : MonoBehaviour
{
    [SerializeField] private float indicatorChangingSpeed;
    [SerializeField] private LevelData[] levels;
    [SerializeField] private Transform textOrigin;
    [SerializeField] private TextMeshProUGUI textPrefab;
    [SerializeField] private PercentIndicator[] indicators;
    [SerializeField] private string detailsBonusKey;
    [SerializeField, Range(0, 1)] private float detailsBonus;

    private GameEnder gameEnder;
    private LevelStarter levelStarter;
    private float waitTime;
    private float timeFromStart;
    private TargetItem[] items;

    [Inject]
    private void Construct(GameEnder ender, LevelStarter l)
    {
        gameEnder = ender;
        levelStarter = l;
        waitTime = levelStarter.LevelDuration;
        items = levels[l.Level - 1].items;
    }

    private void Awake()
    {
        if (SaveManager.instance.HasUpgrade(detailsBonusKey))
        {
            float newVal = 1 - detailsBonus;

            foreach (var item in items)
            {
                item.targetCount = Mathf.RoundToInt(item.targetCount * newVal);
            }
        }

        foreach (var item in items)
        {
            item.text = Instantiate(textPrefab, textOrigin);
            item.textCount = Instantiate(textPrefab, item.text.transform);
            item.textCount.alignment = TextAlignmentOptions.MidlineRight;
        }
        IndicateCount();
        foreach (var item in indicators)
        {
            item.SetValue(0);
        }
    }

    private void Start()
    {
        LocalizationManager.instance.OnLanguageChanged += IndicateCount;
    }

    private void OnDestroy()
    {
        LocalizationManager.instance.OnLanguageChanged -= IndicateCount;
    }

    public void Add(int id, int colorId)
    {
        Debug.Log(id);
        Debug.Log(colorId);
        foreach (var item in items)
        {
            if (item.id == id)
            {
                item.count++;
                Debug.Log(item.count);
                break;
            }
        }
        IndicateCount();

        if (Checktarget())
        {
            gameEnder.Win();
        }
    }


    private void Update()
    {
        if (levelStarter.IsStarted() == false)
        {
            return;
        }
        timeFromStart += Time.deltaTime;

        float p = timeFromStart / waitTime;
        p = Mathf.Clamp01(p);
        foreach (var item in indicators)
        {
            item.SetValue(p);
        }

        if (timeFromStart >= waitTime)
        {
            gameEnder.Lose();
        }
    }

    private void IndicateCount()
    {
        foreach (var item in items)
        {
            item.text.text = LocalizationManager.instance.GetLocalizedValue(item.nameKey) + ": ";
            item.textCount.text = item.count + "/" + item.targetCount;
        }
    }

    private bool Checktarget()
    {
        foreach (var item in items)
        {
            if (item.count < item.targetCount)
            {
                return false;
            }
        }
        return true;
    }   
    
    [System.Serializable]
    private class LevelData
    {
        public TargetItem[] items;
    }

    [System.Serializable]
    private class TargetItem
    {
        public string nameKey;
        public int id;
        [HideInInspector] public int count;
        public int targetCount;
        [HideInInspector] public TextMeshProUGUI text;
        [HideInInspector] public TextMeshProUGUI textCount;
    }

}
