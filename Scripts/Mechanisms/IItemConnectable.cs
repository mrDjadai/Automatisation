using UnityEngine;

public interface IItemConnectable
{
    public void ConnectToInput(ItemPoint innerPoint, ItemPoint outerPoint);
}