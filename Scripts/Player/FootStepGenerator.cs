using UnityEngine;
using System.Collections;

public class FootStepGenerator : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float stepDelay;
    [SerializeField] private AudioClip[] sounds;
    [SerializeField] private PlayerController controller;

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(stepDelay);
            if (controller.IsMoving)
            {
                audioSource.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
            }
        }
    }

}
