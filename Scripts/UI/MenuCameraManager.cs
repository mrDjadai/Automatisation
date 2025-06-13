using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Events;

public class MenuCameraManager : MonoBehaviour
{
    [SerializeField] private CameraPoint[] points;
    [SerializeField] private int activePriority;
    [SerializeField] private int inactivePriority;

    private int current;

    private void Awake()
    {
        points[current].cam.Priority = activePriority;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Escape();
        }
    }

    public void SetCamera(int id)
    {
        if (id == current)
        {
            return;
        }
        points[current].cam.Priority = inactivePriority;
        current = id;
        points[current].cam.Priority = activePriority;
        points[current].onOpen.Invoke();
    }

    public void Escape()
    {
        if (points[current].previousCam != null)
        {
            for (int i = 0; i < points.Length; i++)
            {
                if (points[i].cam == points[current].previousCam)
                {
                    SetCamera(i);
                    return;
                }
            }
        }
    }

    [System.Serializable] private struct CameraPoint
    {
        public CinemachineCamera cam;
        public CinemachineCamera previousCam;
        public UnityEvent onOpen;
    }
}
