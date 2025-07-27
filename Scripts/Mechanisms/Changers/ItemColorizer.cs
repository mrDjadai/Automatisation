using UnityEngine;

public class ItemColorizer : ObjectChanger
{
    [SerializeField] private int colorId;
    [SerializeField] private float changingTime;
    [SerializeField] private ParticleSystem pt;
    [SerializeField] private EaseAudioSourse sourse;
    private bool isChanging;

    private void Start()
    {
        ParticleSystem.MainModule m = pt.main;
        m.startColor = settings.Colors[colorId];
    }

    private void Update()
    {
        if (isChanging != pt.isPlaying || isChanging == IsBroken())
        {
            if (isChanging && IsBroken() == false)
            {
                pt.Play();
                sourse.Play();
            }
            else
            {
                pt.Stop();
                sourse.Stop();
            }
        }
    }

    protected override void OnChangeEnd()
    {
        base.OnChangeEnd();
        pt.Stop();
        sourse.Stop();
        isChanging = false;
    }

    protected override void OnChangeStart()
    {
        base.OnChangeStart();
        pt.Play();
        sourse.Play();
        isChanging = true;
    }

    protected override Item GetNewItem(Item old)
    {
        old.GetComponent<Colorizable>().Colorize(changingTime, colorId);
        return old;
    }
}
