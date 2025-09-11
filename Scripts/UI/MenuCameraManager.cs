using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Events;
using System.Collections;

public class MenuCameraManager : MonoBehaviour
{
    [SerializeField] private CameraPoint[] points;
    [SerializeField] private int activePriority;
    [SerializeField] private int inactivePriority;
    [SerializeField] private float moveTime;

    private int current;
    private ShopItem selectedItem;
    private GazeteItem gazete;

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
    
    public bool SelectGazete(GazeteItem i)
    {
        if (gazete == i)
        {
            return false;
        }

        if (gazete != null)
        {
            DropItem();
        }

        gazete = i;
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
        if (points[current].eventOnEnd == false)
        {
            points[current].onOpen.Invoke();
        }
        else
        {
            StartCoroutine(DoDelayed(points[current].onOpen));
        }
    }

    private IEnumerator DoDelayed(UnityEvent e)
    {
        yield return new WaitForSeconds(moveTime);
        e.Invoke();
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
        
        if (gazete != null)
        {
            gazete.UnSelect();
            gazete = null;
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
        public bool eventOnEnd;
        public CinemachineCamera cam;
        public CinemachineCamera previousCam;
        public UnityEvent onOpen;
    }
}
