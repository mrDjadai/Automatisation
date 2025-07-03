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
    [SerializeField] private Collider[] frictionColliders;
    private Transform _transform;
    protected Rigidbody rb;
    public Coroutine moveCor;

    public void SetPhysicMaterial(PhysicsMaterial material)
    {
        if (frictionColliders[0].material == material)
        {
            return;
        }
        foreach (var item in frictionColliders)
        {
            item.material = material;
        }
    }

    private void Awake()
    {
        _transform = transform;
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        DefaultLayer = gameObject.layer;
    }

    public override void Interact()
    {
        rb.isKinematic = true;
        moveCor = StartCoroutine(GoToPoint(PlayerInventory.instance.HandPoint, () => { }));
        PlayerInventory.instance.SetInHandItem(this);
        OnTake();
    }

    public void Drop()
    {
        StopAllCoroutines();
        _transform.SetParent(null);
        rb.isKinematic = false;
        OnDrop();
        if (moveCor != null)
        {
            StopCoroutine(moveCor);
            moveCor = null;
        }
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

        while (true)
        {
            float stoppingDistance = PlayerInventory.instance.ItemMovingSpeed;
            float distance = Vector3.Distance(_transform.position, target.position);
            targetQuanternion = target.rotation * Quaternion.Euler(itemRotation);

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
        moveCor = null;
    }
}
