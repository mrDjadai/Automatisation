using UnityEngine;
using System.Collections;
using System;
using Zenject;

public class TickSetter : MonoBehaviour
{
    public Action OnTick;
    private float tick;
    private bool active;

    [Inject]
    private void Construct(GameSettings settings)
    {
        tick = settings.TickTime;
    }

    public void Init()
    {
        active = true;
    }

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(tick);
            if (active)
            {
                OnTick?.Invoke();
            }
        }
    }
}
