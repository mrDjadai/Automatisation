using UnityEngine;

public class ConveerRotator : MonoBehaviour
{
    [SerializeField] private Breackable[] breackables;
    [SerializeField] private Transform[] rotatables;
    [SerializeField] private float speed;
    [SerializeField] private EaseAudioSourse audioSourse;

    private void Update()
    {
        foreach (var item in breackables)
        {
            if (item.IsBroken)
            {
                if (audioSourse != null)
                {
                    audioSourse.Stop();

                }
                return;
            }
        }

        float delta = speed * Time.deltaTime;

        foreach (var item in rotatables)
        {
            item.RotateAroundLocal(Vector3.up, delta);
        }
        if (audioSourse != null)
        {
            audioSourse.Play();

        }
    }
}
