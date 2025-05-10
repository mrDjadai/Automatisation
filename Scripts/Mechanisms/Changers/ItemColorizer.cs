using UnityEngine;

public class ItemColorizer : ObjectChanger
{
    [SerializeField] private int colorId;
    [SerializeField] private float changingTime;
    [SerializeField] private ParticleSystem pt;
    [SerializeField] private EaseAudioSourse sourse;

    private void Start()
    {
        ParticleSystem.MainModule m = pt.main;
        m.startColor = settings.Colors[colorId];
    }

    protected override void OnChangeEnd()
    {
        base.OnChangeEnd();
        pt.Stop();
        sourse.Stop();
    }

    protected override void OnChangeStart()
    {
        base.OnChangeStart();
        pt.Play();
        sourse.Play();
    }

    protected override Item GetNewItem(Item old)
    {
        old.GetComponent<Colorizable>().Colorize(changingTime, colorId);
        return old;
    }
}
