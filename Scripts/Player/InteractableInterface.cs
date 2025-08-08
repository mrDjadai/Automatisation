using UnityEngine;

public interface IObjectCounter
{
    public void AddObject(Rigidbody rb);
    public void RemoveObject(Rigidbody rb);
}

public interface IResourse
{
    public void SetSpawner(ResourseSpawner spawner);
    public void OnGarbageDestroy();
}