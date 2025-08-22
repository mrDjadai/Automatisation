using UnityEngine;

public class GazeteBox : MonoBehaviour
{
    [SerializeField] private MenuCameraManager cameraManager;
    [SerializeField] private GazeteItem gazetePrefab;
    [SerializeField] private Sprite[] gazetes;
    [SerializeField] private Transform origin;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    private void Start()
    {
        for (int i = 0; i < SaveManager.instance.LastGazete; i++)
        {
            GazeteItem g = Instantiate(gazetePrefab, origin);
            g.transform.localPosition = i * offset;
            g.Init(gazetes[i], cameraManager, target);
        }
    }
}
