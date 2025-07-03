using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public bool InPuddle;
    public bool IsMoving => inputVelocity.magnitude > 0;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float puddleSpeed;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravity;
    [SerializeField] private float minCameraAngle;
    [SerializeField] private float maxCameraAngle;
    [SerializeField] private Transform raycastPoint;
    [SerializeField] private float raycastLength;
    private CharacterController controller;
    [SerializeField] private Transform cameraTransform;
    private float verticalVelocity;
    private float xRotation = 0f;
    private Vector3 inputVelocity;

    public void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Move();
        RotateCamera();
    }

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        inputVelocity = transform.right * moveX + transform.forward * moveZ;

        Vector3 move = inputVelocity;

        if (IsGrounded())
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * 2f * gravity);
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        if (move.magnitude > 1)
        {
            move = move.normalized;
        }

        move = move * (InPuddle ? puddleSpeed :  moveSpeed) + Vector3.up * verticalVelocity;
        controller.Move(move * Time.deltaTime);
    }

    private bool IsGrounded()
    {
        return controller.isGrounded || (Physics.Raycast(raycastPoint.position, Vector3.down, raycastLength));
    }    
    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * PlayerPrefs.GetFloat("Sensability");
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * PlayerPrefs.GetFloat("Sensability");

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minCameraAngle, maxCameraAngle);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
