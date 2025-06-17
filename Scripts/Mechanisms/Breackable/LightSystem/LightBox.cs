using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class LightBox : PeriodicalBreackable
{
    [SerializeField] private LightLever[] levers;
    private LightActivator activator;
    [SerializeField] private ParticleSystem disableParticles;
    [SerializeField] private AudioSource disableSource;

    private List<int> code;
    private int codePosition;

    [Inject]
    private void Construct(LightActivator a)
    {
        activator = a;
    }

    private void Awake()
    {
        foreach (var item in levers)
        {
            item.Init(this);
        }    
    }

    protected override void OnBreak()
    {
        DisableAll();
        GenerateCode();
        activator.SetActivated(false);
    }

    protected override void OnRepair()
    {
        activator.SetActivated(true);
    }

    private void DisableAll()
    {
        foreach (var item in levers)
        {
            item.SetMode(false);
        }
        disableParticles.Play();
        disableSource.Play();
    }

    public void TryRepair(LightLever lever)
    {
        if (levers[code[codePosition]] != lever)
        {
            codePosition = 0;
            DisableAll();
            return;
        }
        codePosition++;
        foreach (var item in levers)
        {
            if (item.IsActivated == false)
            {
                return;
            }
        }
        Repair();
    }

    private void GenerateCode()
    {
        code = new List<int>();
        codePosition = 0;

        List<int> numbers = new List<int>();
        for (int i = 0; i < levers.Length; i++)
        {
            numbers.Add(i);
        }

        for (int i = 0; i < levers.Length; i++)
        {
            int cur = numbers[Random.Range(0, numbers.Count)];
            code.Add(cur);
            numbers.Remove(cur);
        }
    }    
}
