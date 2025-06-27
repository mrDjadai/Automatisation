using UnityEngine;

public class TutorialDoor : VerticalDoor
{
    [SerializeField] private Breackable[] breakables;

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
        Open();
        enabled = false;
    }
}
