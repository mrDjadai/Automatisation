using UnityEngine;
using DG.Tweening;

public class Item : MonoBehaviour
{
    public int ID => id;
    public bool IsMoving => isMoving;
    public GameSettings Settings => settings;
    [SerializeField] private int id;
    public int ColorID;

    private float movingTime;
    private bool isMoving;
    private Transform tr;
    private ItemPoint curPoint;
    private GameSettings settings;

    private void Awake()
    {
        tr = transform;
    }

    public void Init(GameSettings s)
    {
        settings = s;
        movingTime = s.TickTime;
        GetComponent<Colorizable>().Init();
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
        tr.DORotate(point.Point.eulerAngles, movingTime).SetEase(Ease.Linear);
        return true;
    }   
}
