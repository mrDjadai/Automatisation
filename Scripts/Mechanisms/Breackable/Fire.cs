using UnityEngine;
using DG.Tweening;

public class Fire : PeriodicalBreackable
{
    [SerializeField] private float maxPower;
    [SerializeField] private Light fireLight;
    [SerializeField] private Transform fireObject;
    [SerializeField] private float deltaPower;
    [SerializeField] private float appearTime;

    [SerializeField] private AudioSource startSource;
    [SerializeField] private AudioSource downSource;
    [SerializeField] private EaseAudioSourse source;
    [SerializeField] private float repairOffset;

    private float Power
    {
        get
        {
            return power;
        }
        set
        {
            power = Mathf.Max(0, value);
            fireObject.localScale = Vector3.one * power / maxPower;
            if (power <= repairOffset)
            {
                fireLight.enabled = false;
                Repair();
            }
        }
    }

    private float power;


    private void OnParticleCollision(GameObject other)
    {
        Power -= deltaPower;
        downSource.PlayOneShot(downSource.clip);
    }

    protected override void OnBreak()
    {
        fireLight.enabled = true;
        Power = maxPower;
        fireObject.localScale = Vector3.zero;
        fireObject.DOScale(Vector3.one, appearTime);
        startSource.Play();
        source.Play();
    }

    protected override void OnRepair()
    {
        source.Stop();
    }
}
