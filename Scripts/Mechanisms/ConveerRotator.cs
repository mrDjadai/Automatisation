using UnityEngine;
using Zenject;

public class ConveerRotator : MonoBehaviour
{
    [SerializeField] private Tickable itemConveer;
    private Breackable[] breackables;
    [SerializeField] private Transform[] rotatables;
    [SerializeField] private float speed;
    [SerializeField] private EaseAudioSourse audioSourse;

    private LevelStarter levelStarter;

    [Inject]
    private void Construct(LevelStarter l)
    {
        levelStarter = l;
    }

    private void Awake()
    {
        breackables = itemConveer.breackables;
    }

    private void Update()
    {
        if (levelStarter.IsStarted() == false)
        {
            return;
        }

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
