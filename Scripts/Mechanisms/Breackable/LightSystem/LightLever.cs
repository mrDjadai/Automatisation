using UnityEngine;
using DG.Tweening;

public class LightLever : Interactable
{
    [field : SerializeField] public bool IsActivated { get; private set; }
    [SerializeField] private Transform rotatable;
    [SerializeField] private float rotateAngle;
    [SerializeField] private float rotateTime;
    [SerializeField] private AudioSource clickSource;
    private LightBox lightBox;
    private Tween anim;

    public void Init(LightBox box)
    {
        IsActivated = true;
        lightBox = box;
    }

    public void SetMode(bool m)
    {
        if (IsActivated == m)
        {
            return;
        }
        IsActivated = m;

        if (anim != null)
        {
            anim.Kill();
            anim = null;
        }

        if (m)
        {
            anim = rotatable.DOLocalRotate(new Vector3(m ? rotateAngle : -rotateAngle, 0, 0), rotateTime).OnComplete(() => { 
            lightBox.TryRepair(this);
            });
        }
        else
        {
            anim = rotatable.DOLocalRotate(new Vector3(m ? rotateAngle : -rotateAngle, 0, 0), rotateTime);
        }
    }

    public override void Interact()
    {
        if (IsActivated == false)
        {
            clickSource.Play();
            SetMode(true);
        }
    }

    public override void EndInteract()
    {
    }
}
