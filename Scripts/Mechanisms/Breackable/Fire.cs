using UnityEngine;

public class Fire : PeriodicalBreackable
{
    [SerializeField] private float maxPower;
    [SerializeField] private Light fireLight;
    [SerializeField] private Transform fireObject;
    [SerializeField] private float deltaPower;

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
    }

    protected override void OnRepair()
    {

    }
}
