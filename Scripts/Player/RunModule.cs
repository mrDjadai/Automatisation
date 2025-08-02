using UnityEngine;
using UnityEngine.UI;

public class RunModule : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvas;
    [SerializeField] private Image bar;
    [SerializeField] private float runMultiplier;
    [SerializeField] private float runDuration;
    [SerializeField] private float showSpeed;
    [SerializeField] private float restoreSpeed;

    private float runTime;
    private bool isRunning;
    private bool canRun = true;
    private float targetAlpha;

    [SerializeField] private string activateUpgrade;

    private bool isActive;

    public float GetSpeedMultiplier()
    {
        if (!isActive || !canRun)
        {
            return 1;
        }

        if (runTime >= runDuration)
        {
            return 1;
        }

        if (isRunning)
        {
            return runMultiplier;
        }
        return 1;
    }

    private void Awake()
    {
        canvas.alpha = 0;

        isActive = SaveManager.instance.HasUpgrade(activateUpgrade);
    }

    private void Update()
    {
        isRunning = Input.GetKey(KeyCode.LeftShift);

        targetAlpha = (runTime > 0) ? 1 : 0;

        if (isRunning)
        {
            runTime += Time.deltaTime;
        }
        else
        {
            runTime -= Time.deltaTime * restoreSpeed;
        }
        runTime = Mathf.Clamp(runTime, 0, runDuration);

        if (runTime == runDuration)
        {
            canRun = false;
        }
        else if (runTime == 0)
        {
            canRun = true;
        }

        canvas.alpha = Mathf.MoveTowards(canvas.alpha, targetAlpha, showSpeed * Time.deltaTime);

        bar.fillAmount = 1 - runTime / runDuration;
    }
}
