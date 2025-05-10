using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class EaseAudioSourse : MonoBehaviour
{
    [SerializeField] private float easeTime = 0.5f;

    private AudioSource a;
    private float volume;
    private Coroutine cor;

    private void Awake()
    {
        a = GetComponent<AudioSource>();
        a.loop = true;
        volume = a.volume;
        a.volume = 0;
    }

    public void Play()
    {
        if (a.isPlaying == false)
        {
            a.Play();
        }

        if (cor != null)
        {
            StopCoroutine(cor);
        }

        cor = StartCoroutine(SetVolume(volume));
    }

    public void Stop()
    {
        if (cor != null)
        {
            StopCoroutine(cor);
        }

        cor = StartCoroutine(SetVolume(0));
    }

    private IEnumerator SetVolume(float v)
    {
        while (a.volume != v)
        {
            yield return new WaitForEndOfFrame();
            a.volume = Mathf.MoveTowards(a.volume, v, Time.deltaTime / easeTime);
        }

        if (v == 0)
        {
            a.Stop();
        }
        cor = null;
    }
}
