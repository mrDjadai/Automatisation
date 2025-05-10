using UnityEngine;
using DG.Tweening;

public class TutorialDoor : MonoBehaviour
{
    [SerializeField] private Breackable[] breakables;
    [SerializeField] private InstrumentReturner returner;
    [SerializeField] private Transform door;
    [SerializeField] private Transform openPoint;
    [SerializeField] private float openTime;
    [SerializeField] private AudioSource openSource;

    private void Start()
    {
        foreach (var item in breakables)
        {
            item.Break();
        }
    }

    private void Open()
    {
        returner.gameObject.SetActive(false);
        door.DOMove(openPoint.position, openTime);
        openSource.Play();
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
