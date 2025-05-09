using UnityEngine;

public class BeltPoint : PeriodicalBreackable
{
    public Transform[] Points => connectPoints;
    [SerializeField] private BeltPoint other;
    [SerializeField] private GameObject beltModel;
    [SerializeField] private Transform[] connectPoints = new Transform[2];

    public bool IsPare(BeltPoint p)
    {
        return p == other;
    }

    protected override void OnBreak()
    {
        if (beltModel != null)
        {
            beltModel.SetActive(false);
        }
    }

    protected override void OnRepair()
    {
        if (beltModel != null)
        {
            beltModel.SetActive(true);
        }
        other.Repair();
    }

    private void OnValidate()
    {
        if (connectPoints.Length != 2)
        {
            connectPoints = new Transform[2];
        }
    }
}
