using UnityEngine;
// 1. You must include the new namespace
using UnityEngine.InputSystem; 

public class RigidBodyMovement : MonoBehaviour
{
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;

    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private Rigidbody PlayerBody;
    [Space]
    [SerializeField] private float Speed;
    [SerializeField] private float Sensitivity;
    [SerializeField] private float JumpForce;
    private void Start()
{
    // Locks the cursor to the center of the screen and hides it
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
}
    private void Update()
{
    // Use Keyboard.current for WASD / Arrow movement
    Vector2 move = Vector2.zero;
    if (Keyboard.current != null)
    {
        // Fixed: Appended 'Key' to the arrow key definitions
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) move.y = 1f;
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) move.y = -1f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) move.x = -1f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) move.x = 1f;
    }
    PlayerMovementInput = new Vector3(move.x, 0f, move.y);

    // Use Mouse.current for mouse delta
    if (Mouse.current != null)
    {
        PlayerMouseInput = Mouse.current.delta.ReadValue();
    }

    MovePlayer();
    MovePlayerCamera();
}

    private void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Speed;
        PlayerBody.linearVelocity = new Vector3(MoveVector.x, PlayerBody.linearVelocity.y, MoveVector.z);

        // 4. Use Keyboard.current for the spacebar jump
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            PlayerBody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }

   // Add this private variable at the very top of your class with the other variables
private float xRotation = 0f;

// ... (keep the rest of your variables and methods the same)

private void MovePlayerCamera()
{
    // 1. Calculate mouse look data scaled by sensitivity and time
    // In the New Input System, mouse delta is already pixel-based, so we scale it down slightly
    float mouseX = PlayerMouseInput.x * Sensitivity * 0.05f;
    float mouseY = PlayerMouseInput.y * Sensitivity * 0.05f;

    // 2. Rotate the Camera up and down (X-axis rotation)
    xRotation -= mouseY;
    xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Prevents flipping upside down

    // Apply the rotation to the local transform of the Camera
    PlayerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

    // 3. Rotate the Player Body left and right (Y-axis rotation)
    // We rotate the whole cylinder so "forward" changes direction when looking around
    transform.Rotate(Vector3.up * mouseX);
}
}