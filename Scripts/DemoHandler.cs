using UnityEngine;

public class DemoHandler : MonoBehaviour
{
    public static bool IsDemo { get; private set; }

    [SerializeField] private bool isDemo;
    [SerializeField] private GameObject[] demoDestroyable;

    private void Awake()
    {
        IsDemo = isDemo;

        if (isDemo)
        {
            for (int i = 0; i < demoDestroyable.Length; i++)
            {
                Destroy(demoDestroyable[i]);
            }
        }
    }
}
