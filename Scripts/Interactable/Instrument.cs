using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class Instrument : Interactable
{
    public int DefaultLayer { get; private set; }

    [SerializeField] private Vector3 itemRotation;

    private Transform _transform;
    protected Rigidbody rb;

    private void Awake()
    {
        _transform = transform;
        rb = GetComponent<Rigidbody>();
        DefaultLayer = gameObject.layer;
    }

    public override void Interact()
    {
        rb.isKinematic = true;
        GoToPoint(PlayerInventory.instance.HandPoint, OnTake);
        PlayerInventory.instance.SetInHandItem(this);
    }

    public void Drop()
    {
        StopAllCoroutines();
        _transform.SetParent(null);
        rb.isKinematic = false;
        OnDrop();
    }

    public virtual void Use()
    {
        Debug.Log("Use");
    }

    protected virtual void OnTake()
    {
    }

    protected virtual void OnDrop()
    {
    }

    protected void GoToPoint(Transform target, bool useRotationOffset = true)
    {
        GoToPoint(target, () => { }, useRotationOffset);
    }

    protected void GoToPoint(Transform target, Action onArrive, bool useRotationOffset = true)
    {
        Quaternion targetQuanternion;
        if (useRotationOffset)
        {
            targetQuanternion = target.rotation * Quaternion.Euler(itemRotation);
        }
        else
        {
            targetQuanternion = target.rotation;
        }

        _transform.SetParent(target);
        _transform.localPosition = Vector3.zero;
        _transform.rotation = targetQuanternion;
        onArrive.Invoke();
    }

    public override void EndInteract()
    {
    }
}
