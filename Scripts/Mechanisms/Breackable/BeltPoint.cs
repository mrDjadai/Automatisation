using UnityEngine;

public class BeltPoint : PeriodicalBreackable
{
    public Transform[] Points => connectPoints;
    [SerializeField] private BeltPoint other;
    [SerializeField] private GameObject beltModel;
    [SerializeField] private Transform[] connectPoints = new Transform[2];
    [SerializeField] private AudioSource breakSource;
    [SerializeField] private AudioSource connectSource;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool isMain;

    private void Update()
    {
        if (isMain || other.IsBroken == false)
        {
            transform.RotateAroundLocal(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }

    public void PlaySound()
    {
        connectSource.Play();
    }

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
        breakSource.Play();
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
