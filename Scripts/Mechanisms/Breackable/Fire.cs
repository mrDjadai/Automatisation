using UnityEngine;
using DG.Tweening;

public class Fire : PeriodicalBreackable
{
    [SerializeField] private float maxPower;
    [SerializeField] private Light fireLight;
    [SerializeField] private Transform fireObject;
    [SerializeField] private float deltaPower;
    [SerializeField] private float appearTime;

    private float Power
    {
        get
        {
            return power;
        }
        set
        {
            power = Mathf.Max(0, value);
            fireObject.localScale = Vector3.one * power / maxPower;
            if (power == 0)
            {
                fireLight.enabled = false;
                Repair();
            }
        }
    }

    private float power;


    private void OnParticleCollision(GameObject other)
    {
        Power -= deltaPower;
    }

    protected override void OnBreak()
    {
        fireLight.enabled = true;
        Power = maxPower;
        fireObject.localScale = Vector3.zero;
        fireObject.DOScale(Vector3.one, appearTime);
    }

    protected override void OnRepair()
    {

    }
}
