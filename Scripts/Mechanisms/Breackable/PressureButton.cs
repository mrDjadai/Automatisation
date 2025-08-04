using UnityEngine;
using System.Collections;

public class PressureButton : Breackable
{
    [SerializeField, Range(0, 1)] private float timePercentForDisable;
    [SerializeField] private float upgradeTimeMultiplier;
    [SerializeField] private string upgradeKey;

    private float timeForMax;
    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;
    [SerializeField] private float angleOffset;
    [SerializeField] private Transform rotatable;
    [SerializeField] private Vector3 rotateAxis;
    [SerializeField] private ParticleSystem steam;

    [SerializeField] private AudioSource maxSource;
    [SerializeField] private AudioSource steamSource;
    private float maxVolume;

    private float time;
    private float disableSpeed;

    private IEnumerator Start()
    {
        maxVolume = steamSource.volume;
        steamSource.volume = 0;
        disableSpeed = timeForMax * timePercentForDisable;

        yield return new WaitUntil(Starter.IsStarted);
        while (true)
        {
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
            float angle = minAngle + (maxAngle - minAngle) * (time / timeForMax);

            if (isBroken)
            {
                steamSource.volume = maxVolume * (time / timeForMax);
            }
            else
            {
                steamSource.volume = 0;
            }

            if (time >= timeForMax)
            {
                time = timeForMax;
                angle += Random.Range(-angleOffset, angleOffset);
                if (maxSource.isPlaying == false)
                {
                    maxSource.Play();
                }
                Break();
            }

            rotatable.localEulerAngles = rotateAxis * angle;
        }
    }

    public void OnPress()
    {
        time -= disableSpeed * Time.deltaTime;
        if (time <= 0)
        {
            time = 0;
            Repair();
        }
    }

    protected override void OnBreak()
    {
        steam.Play();
    }

    protected override void OnRepair()
    {
        steam.Stop();
    }

    protected override void OnLoadSettings(Vector3 data)
    {
        timeForMax = data.x;
        if (SaveManager.instance.HasUpgrade(upgradeKey))
        {
            timeForMax *= upgradeTimeMultiplier;
        }
    }
}
