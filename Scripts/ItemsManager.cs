using UnityEngine;
using Zenject;
using TMPro;

public class ItemsManager : MonoBehaviour
{
    [SerializeField] private float indicatorChangingSpeed;
    [SerializeField] private TargetItem[] items;
    [SerializeField] private Transform textOrigin;
    [SerializeField] private TextMeshProUGUI textPrefab;
    [SerializeField] private PercentIndicator[] indicators;

    private GameEnder gameEnder;
    private LevelStarter levelStarter;
    private float waitTime;
    private float timeFromStart;

    [Inject]
    private void Construct(GameEnder ender, LevelStarter l)
    {
        gameEnder = ender;
        levelStarter = l;
        waitTime = levelStarter.LevelDuration;
    }

    private void Awake()
    {
        foreach (var item in items)
        {
            item.text = Instantiate(textPrefab, textOrigin);
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
            item.text.text = LocalizationManager.instance.GetLocalizedValue(item.nameKey) + ": " + item.count + "/" + item.targetCount;
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
    private class TargetItem
    {
        public string nameKey;
        public int id;
        public int count;
        public int targetCount;
        public TextMeshProUGUI text;
    }

}
