using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Events;

public class MenuCameraManager : MonoBehaviour
{
    [SerializeField] private CameraPoint[] points;
    [SerializeField] private int activePriority;
    [SerializeField] private int inactivePriority;

    private int current;
    private ShopItem selectedItem;

    public bool SelectItem(ShopItem i)
    {
        if (selectedItem == i)
        {
            return false;
        }

        if (selectedItem != null)
        {
            DropItem();
        }

        selectedItem = i;
        return true;
    }

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

    public void DropItem()
    {
        if (selectedItem != null)
        {
            selectedItem.Unselect();
            selectedItem = null;
            return;
        }

    }

    public void Escape()
    {
        if (selectedItem != null)
        {
            selectedItem.Unselect();
            selectedItem = null;
            return;
        }

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
