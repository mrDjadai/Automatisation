using UnityEngine;
using System.Collections;

public class SteamPipe : PeriodicalBreackable
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform steam;
    [SerializeField] private Transform point;
    [SerializeField] private float maxScale;
    [SerializeField] private EaseAudioSourse steamSource;
    [SerializeField] private EaseAudioSourse weldingSource;
    [SerializeField] private float weldingTime = 0.2f;
    [SerializeField] private float repairOffset;
    
    private Coroutine cor;

    protected override void OnBreak()
    {
        Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

        steam.position = spawn.position;
        steam.forward = spawn.forward;
        steam.localScale = maxScale * Vector3.one;

        point.position = spawn.position;
        point.gameObject.SetActive(true);
        steamSource.Play();
    }

    public void Repair(float f)
    {
        float scale = Mathf.Max(0, steam.localScale.x - f);

        steam.localScale = scale * Vector3.one;

        if (scale <= repairOffset)
        {
            Repair();
        }

        if (cor != null)
        {
            StopCoroutine(cor);
        }
        weldingSource.Play();
        cor = StartCoroutine(Stop());
    }

    private IEnumerator Stop()
    {
        yield return new WaitForSeconds(weldingTime);
        weldingSource.Stop();
        cor = null;
    }

    protected override void OnRepair()
    {
        point.gameObject.SetActive(false);
        steamSource.Stop();
        weldingSource.Stop();
        if (cor != null)
        {
            StopCoroutine(cor);
        }
        cor = null;
    }
}
