using UnityEngine;
using UnityEngine.InputSystem;

public class XRKeyboardTesting : MonoBehaviour
{
    private Vector3 playerMovementInput;
    private Vector2 playerMouseInput;

    [SerializeField] private Transform playerCamera;
    [SerializeField] private float speed = 3.5f;
    [SerializeField] private float sensitivity = 15f;

    private CharacterController characterController;
    private float xRotation = 0f;
    private float verticalVelocity = 0f; 
    
    // Track whether the cursor is currently locked
    private bool isCursorLocked = true;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        SetCursorState(true);
    }

    private void Update()
    {
        // Toggle cursor lock state when Escape is pressed
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SetCursorState(!isCursorLocked);
        }

        // ONLY look and move if the cursor is locked (playing the game)
        if (isCursorLocked)
        {
            HandleMouseLook();
            HandleMovementInput();
        }
        else
        {
            // Stop movement if menu is open/cursor is freed
            playerMovementInput = Vector3.zero;
        }

        MovePlayerWithPhysics();
    }

    private void SetCursorState(bool lockState)
    {
        isCursorLocked = lockState;
        Cursor.lockState = lockState ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !lockState;
    }

    private void HandleMovementInput()
    {
        Vector2 move = Vector2.zero;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) move.y = 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) move.y = -1f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) move.x = -1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) move.x = 1f;
        }
        playerMovementInput = new Vector3(move.x, 0f, move.y);
    }

    private void HandleMouseLook()
    {
        if (playerCamera == null) return;

        if (Mouse.current != null)
        {
            playerMouseInput = Mouse.current.delta.ReadValue();
        }

        float mouseX = playerMouseInput.x * sensitivity * 0.05f;
        float mouseY = playerMouseInput.y * sensitivity * 0.05f;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f); 

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void MovePlayerWithPhysics()
    {
        if (characterController == null) return;

        Vector3 moveDirection = transform.TransformDirection(playerMovementInput) * speed;

        if (characterController.isGrounded)
        {
            verticalVelocity = -0.5f; 
        }
        else
        {
            verticalVelocity -= 9.81f * Time.deltaTime; 
        }
        
        moveDirection.y = verticalVelocity;
        characterController.Move(moveDirection * Time.deltaTime);
    }
}