using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Instrument : Interactable
{
    public int DefaultLayer { get; private set; }
    public int ID => id;

    [SerializeField] private Vector3 itemRotation;
    [SerializeField] private int id;

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
        StartCoroutine(GoToPoint(PlayerInventory.instance.HandPoint, OnTake));
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

    public override void EndInteract()
    {
    }

    protected IEnumerator GoToPoint(Transform target, Action onArrive)
    {
        Quaternion targetQuanternion;
        targetQuanternion = target.rotation * Quaternion.Euler(itemRotation);

        while (true)
        {
            float stoppingDistance = PlayerInventory.instance.ItemMovingSpeed;
            float distance = Vector3.Distance(_transform.position, target.position);
            if (distance <= stoppingDistance)
            {
                _transform.SetParent(target);
                _transform.localPosition = Vector3.zero;
                _transform.rotation = targetQuanternion;
                onArrive.Invoke();
                break;
            }


            float speed = PlayerInventory.instance.ItemMovingSpeed;
            float rotatingSpeed = PlayerInventory.instance.ItemRotatingSpeed;
            _transform.position = Vector3.Lerp(_transform.position, target.position, speed);
            _transform.rotation = Quaternion.Lerp(_transform.rotation, targetQuanternion, rotatingSpeed);
            yield return new WaitForEndOfFrame();
        }
    }
}
