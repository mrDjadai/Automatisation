using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class HitSoundMaker : MonoBehaviour
{
    private const float inactiveTime = 0.05f;

    [SerializeField] private float maxVolumeSpeed;
    [SerializeField] private AudioClip[] clips;

    private float volumeMultiplier;
    private Rigidbody _rigidbody;
    private AudioSource audioSource;
    private bool canMakeSound;

    private void Awake()
    {
        volumeMultiplier = 1 / maxVolumeSpeed;
        _rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(inactiveTime);
        canMakeSound = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (canMakeSound == false)
        {
            return;
        }
        audioSource.volume = _rigidbody.linearVelocity.magnitude * volumeMultiplier;
        audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }
}
