using UnityEngine;

public class Water : Instrument
{
    [SerializeField] private ParticleSystem pSystem;
    private bool curMode;

    private void Start()
    {
        SetActiveMode(false);
    }

    public override void Use()
    {
        SetActiveMode(!curMode);
    }

    protected override void OnDrop()
    {
        base.OnDrop();
        SetActiveMode(false);
    }

    private void SetActiveMode(bool mode)
    {
        curMode = mode;
        ParticleSystem.EmissionModule mod = pSystem.emission;
        mod.enabled = mode;
        if (mode)
        {
            pSystem.Play();
        }
    }
}
