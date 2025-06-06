using UnityEngine;
using Zenject;

public class InstrumentReturner : MonoBehaviour
{
    [SerializeField] private Transform playerSpawn;
    private PlayerController player;

    [Inject]
    private void Construct(PlayerController p)
    {
        player = p;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            other.GetComponent<CharacterController>().enabled = false;
            other.transform.position = playerSpawn.position;
            other.GetComponent<CharacterController>().enabled = true;
        }
        else
        {
            if (other.attachedRigidbody && other.attachedRigidbody.GetComponent<Instrument>())
            {
                other.attachedRigidbody.transform.position = player.transform.position;
            }
        }
    }
}
