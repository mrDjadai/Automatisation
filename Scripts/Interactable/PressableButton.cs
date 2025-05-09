using UnityEngine;
using UnityEngine.Events;

public class PressableButton : Interactable
{
    [SerializeField] private UnityEvent onPress;

    private bool isPressed;

    public override void EndInteract()
    {
        isPressed = false;
    }

    public override void Interact()
    {
        isPressed = true;
    }

    private void Update()
    {
        if (isPressed)
        {
            onPress.Invoke();
        }
    }
}
