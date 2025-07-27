using UnityEngine;
using System.Collections;
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
    }

    private IEnumerator Start()
    {
        float t = startDelay;
        int second = startDelay;
        int second1;
        timeText.text = Mathf.RoundToInt(startDelay).ToString();

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
