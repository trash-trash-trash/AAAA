using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementHandler : MonoBehaviour
{
    [Header("References")]
    public Transform cameraRoot; // camera child GO
    public float moveSpeed = 5f;
    public float turnSpeed = 10f;
    public float mouseSensitivity = 2f;

    private Rigidbody rb;
    private Vector2 moveInput;
    private float xRotation = 0f;
    private Vector3 lastMoveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        var inputHandler = GetComponent<PlayerInputHandler>();
        if (inputHandler != null)
            inputHandler.AnnounceMoveVector2 += SetMoveInput;
    }

    void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }

    void FixedUpdate()
    {
        Move();
        RotateCamera();
    }

    void Update()
    {
        //LookAround();
    }

    void Move()
    {
        Vector3 moveDir = transform.forward * moveInput.y + transform.right * moveInput.x;
        moveDir = moveDir.normalized;

        if (moveDir.magnitude > 0.1f)
        {
            lastMoveDirection = moveDir; // store for rotation
        }

        Vector3 targetVelocity = new Vector3(moveDir.x * moveSpeed, rb.linearVelocity.y, moveDir.z * moveSpeed);
        rb.linearVelocity = targetVelocity;
    }

    void LookAround() // can use this if we want camera to rotate with mouse later 
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue() * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseDelta.x);
        xRotation -= mouseDelta.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraRoot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
    
    void RotateCamera()
        {
            if (lastMoveDirection.magnitude > 0.1f)
            {
                // rotate only if moving forward (dot > 0)
                float forwardDot = Vector3.Dot(transform.forward, lastMoveDirection);
                if (forwardDot > 0f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(lastMoveDirection);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime);
                }
            }

            // fixed camera's local rotation
            cameraRoot.localRotation = Quaternion.identity;
        }
}
