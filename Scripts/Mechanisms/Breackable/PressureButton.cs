using UnityEngine;
using System.Collections;

public class PressureButton : Breackable
{
    [SerializeField] private float timeForMax;
    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;
    [SerializeField] private float angleOffset;
    [SerializeField] private Transform rotatable;
    [SerializeField] private Vector3 rotateAxis;
    [SerializeField] private float disableSpeed;
    [SerializeField] private ParticleSystem steam;

    [SerializeField] private AudioSource maxSource;
    [SerializeField] private AudioSource steamSource;
    private float maxVolume;

    private float time;

    private IEnumerator Start()
    {
        maxVolume = steamSource.volume;
        steamSource.volume = 0;

        while (true)
        {
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
            float angle = minAngle + (maxAngle - minAngle) * (time / timeForMax);
            steamSource.volume = maxVolume * (time / timeForMax);
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
}
