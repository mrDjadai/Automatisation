using UnityEngine;

public class ItemColorizer : ObjectChanger
{
    [SerializeField] private int[] colorId;
    [SerializeField] private float changingTime;
    [SerializeField] private ParticleSystem[] pt;
    [SerializeField] private EaseAudioSourse sourse;
    private bool isChanging;

    private void Start()
    {
        for (int i = 0; i < pt.Length; i++)
        {
            ParticleSystem.MainModule m = pt[i].main;
            m.startColor = settings.Colors[colorId[i]];
        }
    }

    private void Update()
    {
        if (isChanging != pt[0].isPlaying || isChanging == IsBroken())
        {
            if (isChanging && IsBroken() == false)
            {
                foreach (var item in pt)
                {
                    item.Play();
                }
                sourse.Play();
            }
            else
            {
                foreach (var item in pt)
                {
                    item.Stop();
                }
                sourse.Stop();
            }
        }
    }

    protected override void OnChangeEnd()
    {
        base.OnChangeEnd();
        foreach (var item in pt)
        {
            item.Stop();
        }
        sourse.Stop();
        isChanging = false;
    }

    protected override void OnChangeStart()
    {
        base.OnChangeStart();
        foreach (var item in pt)
        {
            item.Play();
        }
        sourse.Play();
        isChanging = true;
    }

    protected override Item GetNewItem(Item old)
    {
        old.GetComponent<Colorizable>().Colorize(changingTime, colorId);
        return old;
    }
}
