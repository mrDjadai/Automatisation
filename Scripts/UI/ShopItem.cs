using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private MenuCameraManager cameraManager;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private float animationTime;
    [SerializeField] private float activateTime;
    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private UpgradeUnlocker unlocker;

    private Vector3 pos;
    private Quaternion rot;
    private Transform tr;
    private Coroutine cor;
    private Tween tween;

    private void Start()
    {
        tr = transform;
        pos = tr.position;
        rot = tr.rotation;

        canvasGroup.alpha = 0;
    }

    public void Unselect()
    {
        if (cor != null)
        {
            StopCoroutine(cor);
        }

        cor = StartCoroutine(SetVisibility(false, () => {
            Move(pos, rot, null);
            canvasGroup.interactable = false;
        }));
        unlocker.Hide();
    }

    private void OnMouseUpAsButton()
    {
        if (cameraManager.SelectItem(this))
        {
            if (cor != null)
            {
                StopCoroutine(cor);
            }
            Move(targetPoint.position, targetPoint.rotation, () => { 
                cor = StartCoroutine(SetVisibility(true, null)); 
                canvasGroup.interactable = true;
            });
        }
    }

    private IEnumerator SetVisibility(bool v, Action onComplete)
    {
        float t = 0;
        float target = v ? 1 : 0;
        float startValue = 1 - target;

        while (t < activateTime)
        {
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startValue, target, t / activateTime);
        }
        canvasGroup.alpha = target;
        onComplete?.Invoke();
    }   
    
    private void Move(Vector3 p, Quaternion r, Action onComplete)
    {
        if (tween != null)
        {
            tween.Kill();
        }

        tr.DOMove(p, animationTime);
        tween = tr.DORotateQuaternion(r, animationTime).OnComplete(() => { onComplete?.Invoke(); });
    }
}
