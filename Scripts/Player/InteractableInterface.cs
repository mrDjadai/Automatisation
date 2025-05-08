using UnityEngine;
public interface IInteractable
{
    public void Interact();
}

public interface IObjectCounter
{
    public void AddObject(Rigidbody rb);
    public void RemoveObject(Rigidbody rb);
}