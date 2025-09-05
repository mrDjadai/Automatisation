using System.Collections;
using UnityEngine;

public class TutorialDoor : VerticalDoor
{
    [SerializeField] private Breackable[] breakables;
    [SerializeField] private float delay;

    private void Start()
    {
        foreach (var item in breakables)
        {
            item.Break();
        }
    }

    private void Update()
    {
        foreach (var item in breakables)
        {
            if (item.IsBroken)
            {
                return;
            }
        }
        StartCoroutine(DelayedOpen());
        enabled = false;
    }

    private IEnumerator DelayedOpen()
    {
        yield return new WaitForSeconds(delay);
        Open();
    }
}
