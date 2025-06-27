using UnityEngine;
using DG.Tweening;

public class VerticalDoor : MonoBehaviour
{
    [SerializeField] private InstrumentReturner returner;
    [SerializeField] private Transform door;
    [SerializeField] private Transform openPoint;
    [SerializeField] private float openTime;
    [SerializeField] private AudioSource openSource;

    public void Open()
    {
        returner.gameObject.SetActive(false);
        door.DOMove(openPoint.position, openTime);
        openSource.Play();
    }
}
