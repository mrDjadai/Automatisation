using UnityEngine;
using UnityEngine.UI;

public class SliderGear : MonoBehaviour
{
    [SerializeField] private Transform rotatable;
    [SerializeField] private Vector3 rotateAxis;
    [SerializeField] private Slider slider;
    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;

    private void Awake()
    {
        slider.onValueChanged.AddListener(SetValue);    
    }

    private void SetValue(float val)
    {
        float angle = slider.normalizedValue * (maxAngle - minAngle) + minAngle;
        rotatable.localEulerAngles = rotateAxis * angle;    
    }
}
