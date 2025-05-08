using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    [SerializeField] InputAction[] actions;
    [SerializeField] InputAction[] buttonUpActions;

    private void Update()
    {
        foreach (var item in actions)
        {
            if (Input.GetKeyDown(item.key))
            {
                item.action.Invoke();
            }
        }

        foreach (var item in buttonUpActions)
        {
            if (Input.GetKeyUp(item.key))
            {
                item.action.Invoke();
            }
        }
    }
}

[System.Serializable]
public class InputAction
{
    public KeyCode key;
    public UnityEvent action;
}
