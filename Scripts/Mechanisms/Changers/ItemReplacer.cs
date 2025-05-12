using UnityEngine;
using DG.Tweening;

public class ItemReplacer : ObjectChanger
{
    [SerializeField] private Item newItem;
    [SerializeField] private Transform[] simulated1;
    [SerializeField] private Transform[] simulated2;
    [SerializeField] private float animationTime;
    [SerializeField] private AudioSource createSource;
    [SerializeField] protected Vector3 openAnle;

    protected override Item GetNewItem(Item old)
    {
        Item nItem = Instantiate(newItem, old.transform.position, old.transform.rotation);
        nItem.Init(settings);
        Destroy(old.gameObject);
        createSource.Play();
        return nItem;
    }

    protected override void OnChangeStart()
    {
        base.OnChangeStart();

        foreach (var item in simulated1)
        {
            item.DOLocalRotate(openAnle, animationTime / 2).OnComplete(() => { item.DOLocalRotate(Vector3.zero, animationTime / 2); });
        }
    }

    protected override void OnChangeEnd()
    {
        base.OnChangeEnd();
        foreach (var item in simulated2)
        {
            item.DOLocalRotate(openAnle, animationTime / 2).OnComplete(() => { item.DOLocalRotate(Vector3.zero, animationTime / 2); });
        }
    }
}
