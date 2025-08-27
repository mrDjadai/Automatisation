using UnityEngine;
using Zenject;

public class Conveer : Tickable
{
    [SerializeField] private Vector3 localForce;
    [SerializeField] private Tickable itemConveer;
    private Vector3 force;

    private LevelStarter levelStarter;

    [Inject]
    private void Construct(LevelStarter l)
    {
        levelStarter = l;
    }

    protected override void OnTick()
    {
    }

    private void Awake()
    {
        force = transform.localToWorldMatrix.MultiplyVector(localForce);
        breackables = itemConveer.breackables;

    }

    private void OnTriggerStay(Collider other)
    {
        if (IsBroken() || levelStarter.IsStarted() == false)
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
