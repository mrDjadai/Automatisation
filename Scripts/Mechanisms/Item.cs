using UnityEngine;
using DG.Tweening;

public class Item : MonoBehaviour
{
    public int ID => id;
    public bool IsMoving => isMoving;
    [SerializeField] private int id;

    private float movingTime;
    private bool isMoving;
    private Transform tr;
    private ItemPoint curPoint;

    private void Awake()
    {
        tr = transform;
    }

    public void Init(float mTime)
    {
        movingTime = mTime;
    }

    public bool Move(ItemPoint point)
    {
        if (isMoving || point.IsEmpty == false)
        {
            return false;
        }
        if (curPoint != null)
        {
            curPoint.Pop();
        }
        point.Push(this);
        curPoint = point;
        isMoving = true;
        tr.DOMove(point.Point.position, movingTime).SetEase(Ease.Linear).OnComplete(() => { isMoving = false; });
        return true;
    }   
}
