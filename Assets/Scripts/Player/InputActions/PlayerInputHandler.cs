using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerActions controls;
    private Vector2 moveInput;

    public event Action<Vector2> AnnounceMoveVector2;
    public event Action<bool> AnnounceInteract;

    private void Awake()
    {
        controls = new PlayerActions();
        controls.InGameActions.Move.performed += OnMove;
        controls.InGameActions.Move.canceled += OnMove;

        controls.InGameActions.Interact.performed += OnInteract;
        controls.InGameActions.Interact.canceled += OnInteract;
    }

    private void OnInteract(InputAction.CallbackContext obj)
    {
        if(obj.canceled)
            AnnounceInteract?.Invoke(false);
        else
            AnnounceInteract?.Invoke(true);
    }

    private void OnEnable()
    {
        controls.Enable();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
            moveInput = context.ReadValue<Vector2>();
        else
            moveInput = Vector2.zero;
        
        HandleMove();
    }

    private void HandleMove()
    {
        Vector2 normalized = moveInput.normalized;
        Vector2 clampedMove = new Vector2(
            Mathf.RoundToInt(normalized.x),
            Mathf.RoundToInt(normalized.y)
        );

        moveInput = clampedMove;
        AnnounceMoveVector2?.Invoke(moveInput);
    }
    
    void OnDisable()
    {
        controls.Disable();

        controls.InGameActions.Move.performed -= OnMove;
        controls.InGameActions.Move.canceled -= OnMove;
        controls.InGameActions.Interact.performed -= OnInteract;
        controls.InGameActions.Interact.canceled -= OnInteract;
    }
}