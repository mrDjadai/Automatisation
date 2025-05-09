using UnityEngine;

public class Conveer : Tickable
{
    [SerializeField] private Vector3 localForce;

    private Vector3 force;

    protected override void OnTick()
    {
    }

    private void Awake()
    {
        force = transform.localToWorldMatrix.MultiplyVector(localForce);    
    }

    private void OnTriggerStay(Collider other)
    {
        if (IsBroken())
        {
            return;
        }
        if (other.attachedRigidbody)
        {
            other.attachedRigidbody.AddForce(force);
        }

        if (other.gameObject.TryGetComponent<CharacterController>(out CharacterController c))
        {
            c.Move(force * Time.deltaTime);
        }
    }

}
