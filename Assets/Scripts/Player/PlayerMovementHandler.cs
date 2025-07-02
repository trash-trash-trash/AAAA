using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementHandler : MonoBehaviour
{
    [Header("References")]
    public Transform cameraRoot; // camera child GO
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;

    private Rigidbody rb;
    private Vector2 moveInput;
    private float xRotation = 0f;

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
    }

    void Update()
    {
        LookAround();
    }

    void Move()
    {
        Vector3 moveDir = transform.forward * moveInput.y + transform.right * moveInput.x;
        Vector3 targetVelocity = moveDir * moveSpeed;

        // preserving y velocity in case we want to jump later or something :3
        Vector3 velocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
        rb.linearVelocity = velocity;
    }

    void LookAround()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue() * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseDelta.x);
        xRotation -= mouseDelta.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraRoot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
