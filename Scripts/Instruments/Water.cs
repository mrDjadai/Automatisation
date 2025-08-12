using UnityEngine;
using DG.Tweening;

public class Water : Instrument
{
    [SerializeField] private ParticleSystem pSystem;
    [SerializeField] private EaseAudioSourse audioSourse;
    [SerializeField] private Vector3 openAngle;
    [SerializeField] private Vector3 closeAngle;
    [SerializeField] private Transform rotatable;
    [SerializeField] private float rotateTime = 0.5f;
    [SerializeField] private WaterStrength[] upgrades;

    [SerializeField] private AudioSource useSource;
    private bool curMode;

    private void Start()
    {
        SetActiveMode(false);
        foreach (var item in upgrades)
        {
            if (SaveManager.instance.HasUpgrade(item.key))
            {
                ParticleSystem.MainModule main = pSystem.main;
                main.startSpeedMultiplier *= item.multiplier;
                main.startLifetimeMultiplier *= item.multiplier;
            }
        }
    }

    public override void Use()
    {
        SetActiveMode(!curMode);
        useSource.Play();
    }

    protected override void OnDrop()
    {
        base.OnDrop();
        SetActiveMode(false);
    }

    private void SetActiveMode(bool mode)
    {
        curMode = mode;
        if (mode)
        {
            pSystem.Play();
            audioSourse.Play();
            rotatable.DOLocalRotate(openAngle, rotateTime);
        }
        else
        {
            pSystem.Stop();
            audioSourse.Stop();
            rotatable.DOLocalRotate(closeAngle, rotateTime);
        }
    }

    [System.Serializable]
    private struct WaterStrength
    {
        public string key;
        public float multiplier;
    }
}
