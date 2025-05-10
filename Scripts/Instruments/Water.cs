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

    private bool curMode;

    private void Start()
    {
        SetActiveMode(false);
    }

    public override void Use()
    {
        SetActiveMode(!curMode);
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
}
