using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public bool IsMoving => inputVelocity.magnitude > 0;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float puddleSpeed;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravity;
    [SerializeField] private float minCameraAngle;
    [SerializeField] private float maxCameraAngle;
    [SerializeField] private Transform raycastPoint;
    [SerializeField] private Transform raycastPointHead;
    [SerializeField] private float raycastLength;
    [SerializeField] private RunModule runModule;
    [SerializeField] private string longPuddleKey;
    [SerializeField] private float longPuddleDuration;
    [SerializeField] private GameEnder gameEnder;

    private bool inPuddle;
    private bool useLongPuddle;
    private Coroutine puddleCor;

    private CharacterController controller;
    [SerializeField] private Transform cameraTransform;
    private float verticalVelocity;
    private float xRotation = 0f;
    private Vector3 inputVelocity;

    public void Start()
    {
        controller = GetComponent<CharacterController>();

        useLongPuddle = SaveManager.instance.HasUpgrade(longPuddleKey);
    }

    private void Update()
    {
        Move();
        RotateCamera();
        CheckCeiling();
    }

    private void CheckCeiling()
    {
        if (controller.velocity.y > 0)
        {
            if (Physics.Raycast(raycastPointHead.position, Vector3.up, raycastLength))
            {
                verticalVelocity = 0;
            }
        }
    }

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        if (!gameEnder.IsEnded)
        {
            inputVelocity = transform.right * moveX + transform.forward * moveZ;
        }

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

        move = move * GetSpeed()
            + Vector3.up * verticalVelocity;

        controller.Move(move * Time.deltaTime);
    }

    private float GetSpeed()
    {
        float speed = (inPuddle ? puddleSpeed : moveSpeed) * runModule.GetSpeedMultiplier();
        if (PlayerInventory.instance.InHandItem != null)
        {
            speed *= PlayerInventory.instance.InHandItem.SpeedMultiplier;
        }
        return speed;
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

    public void OnPuddleEnter()
    {
        if (puddleCor != null)
        {
            StopCoroutine(puddleCor);
        }
        inPuddle = true;
    }

    public void OnPuddleExit()
    {
        if (useLongPuddle)
        {
            puddleCor = StartCoroutine(LongPuddle());
        }
        else
        {
            inPuddle = false;
        }
    }

    private IEnumerator LongPuddle()
    {
        yield return new WaitForSeconds(longPuddleDuration);
        inPuddle = false;
    }

    private void OnDisable()
    {
        inputVelocity = Vector3.zero;
    }
}
