using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;

public class ItemsManager : MonoBehaviour
{
    [SerializeField] private Image waitIndicator;
    [SerializeField] private float waitTime;
    [SerializeField] private float indicatorChangingSpeed;
    [SerializeField] private TargetItem[] items;
    [SerializeField] private Transform textOrigin;
    [SerializeField] private TextMeshProUGUI textPrefab;

    private float lastEnterTime;
    private GameEnder gameEnder;
    private LevelStarter levelStarter;

    [Inject]
    private void Construct(GameEnder ender, LevelStarter l)
    {
        gameEnder = ender;
        levelStarter = l;
    }

    private void Awake()
    {
        lastEnterTime = Time.time;
        foreach (var item in items)
        {
            item.text = Instantiate(textPrefab, textOrigin);
        }
        IndicateCount();
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
            if (item.id == id && item.colorId == colorId)
            {
                item.count++;
                Debug.Log(item.count);
                break;
            }
        }
        lastEnterTime = Time.time;
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
        float deltaTime = Time.time - lastEnterTime;

        float targetVal = deltaTime / waitTime;

        waitIndicator.fillAmount = Mathf.MoveTowards(waitIndicator.fillAmount, targetVal, indicatorChangingSpeed * Time.deltaTime);

        if (deltaTime >= waitTime)
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
        public int colorId;
        public int id;
        public int count;
        public int targetCount;
        public TextMeshProUGUI text;
    }

}
