using UnityEngine;
using System.Collections;
using System;
using Zenject;

public class TickSetter : MonoBehaviour
{
    public Action OnTick;
    private float tick;

    [Inject]
    private void Construct(GameSettings settings)
    {
        tick = settings.TickTime;
    }

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(tick);
            OnTick?.Invoke();
        }
    }
}
