using UnityEngine;

public class SteamPipe : PeriodicalBreackable
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform steam;
    [SerializeField] private Transform point;
    [SerializeField] private float maxScale;

    protected override void OnBreak()
    {
        Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

        steam.position = spawn.position;
        steam.forward = spawn.forward;
        steam.localScale = maxScale * Vector3.one;

        point.position = spawn.position;
        point.gameObject.SetActive(true);
    }

    public void Repair(float f)
    {
        float scale = Mathf.Max(0, steam.localScale.x - f);

        steam.localScale = scale * Vector3.one;

        if (scale == 0)
        {
            Repair();
        }
    }

    protected override void OnRepair()
    {
        point.gameObject.SetActive(false);
    }
}
