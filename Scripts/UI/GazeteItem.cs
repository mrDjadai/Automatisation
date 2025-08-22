using UnityEngine;
using DG.Tweening;

public class GazeteItem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float moveTime;
    private MenuCameraManager cameraManager;
    private Transform targetPoint;
    private Transform tr;
    private Vector3 pos;

    private void Start()
    {
        tr = transform;
        pos = tr.localPosition;
    }

    public void Select()
    {
        tr.DOMove(targetPoint.position, moveTime);
        tr.DORotate(targetPoint.eulerAngles, moveTime);
    }

    public void UnSelect()
    {
        tr.DOLocalMove(pos, moveTime);
        tr.DOLocalRotate(Vector3.zero, moveTime);
    }   
    
    public void Init(Sprite s, MenuCameraManager m, Transform point)
    {
        spriteRenderer.sprite = s;
        cameraManager = m;
        targetPoint = point;
    }

    private void OnMouseUpAsButton()
    {
        if (cameraManager.SelectGazete(this))
        {
            Select();
        }
    }
}
