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
    [SerializeField] private Difficult[] difficults;
    [SerializeField] private Image gazete;
    [SerializeField] private Sprite[] gazeteSprites;
    [SerializeField] private Button startButton;
    [SerializeField] private TMP_Text startButtonText;
    [SerializeField] private float gazeteAnimationTime;
    [SerializeField] private float gazeteAnimationAngle;
    [SerializeField] private float buttonShowDelay;
    [SerializeField] private float buttonShowTime;
    [SerializeField] private CanvasGroup gazeteGroup;

    private bool isStarted;

    [Inject]
    private void Construct(TickSetter t, LightActivator a)
    {
        tickSetter = t;
        lightActivator = a;
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
        int level = PlayerPrefs.GetInt("CurrentLevel");
        Debug.Log("Loaded level" + level);
        CurrentDifficult = difficults[level - 1];

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
        gazete.sprite = gazeteSprites[level - 1];
        startButton.onClick.AddListener(StartGame);
        startButton.image.color = Color.clear;
        Color tColor = startButtonText.color;
        tColor.a = 0;
        startButtonText.color = tColor;
        startButton.interactable = false;
    }

    private void Start()
    {
        gazete.rectTransform.DOScale(Vector2.one, gazeteAnimationTime);
        gazete.rectTransform.DOLocalRotate(Vector3.forward * gazeteAnimationAngle, gazeteAnimationTime);
        startButton.image.DOColor(Color.white, buttonShowTime).SetDelay(buttonShowDelay).OnComplete(() => { startButton.interactable = true; });
        Color tColor = startButtonText.color;
        tColor.a = 1;
        startButtonText.DOColor(tColor, buttonShowTime).SetDelay(buttonShowDelay);
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

        scalableIndicator.DOScale(Vector3.one * maxScale, animationTime1).OnComplete(() =>
        {
            scalableIndicator.DOScale(Vector3.zero, animationTime2);
        });
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
}
