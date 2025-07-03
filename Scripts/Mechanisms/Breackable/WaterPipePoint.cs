using UnityEngine;

public class WaterPipePoint : MonoBehaviour
{
    [SerializeField] private WaterPipe pipe;
    [SerializeField] private Collider col;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private EaseAudioSourse source;
    [SerializeField] private AudioSource repairSource;

    private GameObject repairVisualisator;

    public void Repair(GameObject repairer)
    {
        if (repairVisualisator != null)
        {
            Destroy(repairVisualisator);
        }
        repairVisualisator = repairer;
        col.enabled = false;
        particles.Stop();
        pipe.Repair();
        source.Stop();
        repairSource.Play();
    }

    public void Break()
    {
        if (repairVisualisator != null)
        {
            Destroy(repairVisualisator);
        }
        col.enabled = true;
        particles.Play();
        source.Play();
    }
}
