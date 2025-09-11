using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Zenject;
using System.Linq;

public class LevelStarter : MonoBehaviour
{
    public Difficult CurrentDifficult { get; private set; }
    public float LevelDuration { get; private set;}
    public LevelData CurLevel => curLevel;
    public int Level { get; private set; }
    private LightActivator lightActivator;
    private TickSetter tickSetter;

    [SerializeField] private int startDelay;

    [SerializeField] private Transform scalableIndicator;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private float animationTime1;
    [SerializeField] private float animationTime2;
    [SerializeField] private float maxScale;
    [SerializeField] private float punchScale;
    [SerializeField] private float punchDuration;
    [SerializeField] private int startPunch;
    [SerializeField] private AudioSource punchSource;
    [SerializeField] private WallUnit[] wallUnits;
    [SerializeField] private ObjectUnit[] objectUnits;
    [SerializeField] private LevelData[] levels;
    [SerializeField] private Image gazete;
    [SerializeField] private Button startButton;
    [SerializeField] private TMP_Text startButtonText;
    [SerializeField] private float gazeteAnimationTime;
    [SerializeField] private float gazeteAnimationAngle;
    [SerializeField] private float buttonShowDelay;
    [SerializeField] private float buttonShowTime;
    [SerializeField] private CanvasGroup gazeteGroup;
    [SerializeField] private UpgradeLevel[] upgrades;

    private bool isStarted;
    private LevelData curLevel;

    [Inject]
    private void Construct(TickSetter t, LightActivator a)
    {
        tickSetter = t;
        lightActivator = a;

        Init();
    }

    public bool IsStarted()
    {
        return isStarted;
    }

    public void FoceStart()
    {
        if (isStarted == false)
        {
            StopAllCoroutines();
            Activate();
        }
    }

    public void Init()
    {
        Level = PlayerPrefs.GetInt("CurrentLevel");
        Debug.Log("Loaded level" + Level);

        if (Level != 0)
        {
            LoadLevel(Level);
        }
        else
        {
            LoadTutorial();
        }
    }

    private void LoadLevel(int level)
    {
        curLevel = levels[level - 1];
        CurrentDifficult = curLevel.difficult;
        LevelDuration = curLevel.duration;

        foreach (var item in wallUnits)
        {
            if (item.openLevels.Contains(level))
            {
                item.door.Open();
            }
        }

        foreach (var item in objectUnits)
        {
            item.activatable.SetActive(item.activeLevels.Contains(level));
        }

        gazete.rectTransform.localScale = Vector2.zero;
        gazete.sprite = curLevel.gazete;
        startButton.onClick.AddListener(StartGame);
        startButton.image.color = Color.clear;
        Color tColor = startButtonText.color;
        tColor.a = 0;
        startButtonText.color = tColor;
        startButton.interactable = false;

        foreach (var item in upgrades)
        {
            if (SaveManager.instance.HasUpgrade(item.upgradeKey))
            {
                startDelay += item.bonusPrepareTime;
            }
        }
    }

    private void LoadTutorial()
    {
        CurrentDifficult = levels[0].difficult;

        gazeteGroup.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Activate();
    }

    private void Start()
    {
        if (DemoHandler.IsDemo || SaveManager.instance.LastGazete >= Level)
        {
            if (Level != 0)
            {
                StartGame();
            }
        }
        else
        {
            SaveManager.instance.LastGazete = Level;

            gazete.rectTransform.DOScale(Vector2.one, gazeteAnimationTime);
            gazete.rectTransform.DOLocalRotate(Vector3.forward * gazeteAnimationAngle, gazeteAnimationTime);
            startButton.image.DOColor(Color.white, buttonShowTime).SetDelay(buttonShowDelay).OnComplete(() => { startButton.interactable = true; });
            Color tColor = startButtonText.color;
            tColor.a = 1;
            startButtonText.DOColor(tColor, buttonShowTime).SetDelay(buttonShowDelay);
        }
    }

    private void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(Starting());
    }   
    
    private IEnumerator Starting()
    {

        timeText.text = Mathf.RoundToInt(startDelay).ToString();

        float t = buttonShowTime;

        while (t > 0)
        {
            yield return new WaitForEndOfFrame();
            t -= Time.deltaTime;
            gazeteGroup.alpha = t / buttonShowTime;
        }
        gazeteGroup.gameObject.SetActive(false);

        t = startDelay;
        int second = startDelay;
        int second1;
        while (t > 0)
        {
            yield return new WaitForEndOfFrame();
            t -= Time.deltaTime;
            second1 = Mathf.CeilToInt(t);
            if (second != second1)
            {
                if (second1 > 0 &&  second1 <= startPunch)
                {
                    scalableIndicator.DOPunchScale(Vector3.one * punchScale, punchDuration);
                    punchSource.Play();
                }
                second = second1;
            }
            timeText.text = second.ToString();
        }
        Activate();
    }

    private void Activate()
    {
        isStarted = true;

        lightActivator.SetActivated(true);


        tickSetter.Init();

        if (PlayerPrefs.GetInt("CurrentLevel") != 0)
        {
            scalableIndicator.DOScale(Vector3.one * maxScale, animationTime1).OnComplete(() =>
            {
                scalableIndicator.DOScale(Vector3.zero, animationTime2);
            });
        }
        else
        {
            scalableIndicator.localScale = Vector3.zero;
        }
    }

    [System.Serializable]
    private struct WallUnit
    {
        public VerticalDoor door;
        public int[] openLevels;
    }

    [System.Serializable]
    private struct ObjectUnit
    {
        public GameObject activatable;
        public int[] activeLevels;
    }

    [System.Serializable]
    private struct UpgradeLevel
    {
        public string upgradeKey;
        public int bonusPrepareTime;
    }
}
