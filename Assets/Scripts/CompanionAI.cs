using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CompanionAI : MonoBehaviour
{
    [Header("Target Setup")]
    public Transform playerTarget;

    [Header("Movement Settings")]
    public float moveSpeed = 3.0f;
    public float rotationSpeed = 5.0f;
    public float stoppingDistance = 2.5f;
    public float gravity = 9.81f;

    private CharacterController controller;
    private float verticalVelocity = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (controller == null)
            Debug.LogError("CompanionAI: No CharacterController found on " + gameObject.name);

        if (playerTarget == null)
            Debug.LogWarning("CompanionAI: Player Target is not assigned in the Inspector.");
    }

    void Update()
    {
        if (playerTarget == null || controller == null) return;

        HandleRotation();
        HandleMovement();
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = playerTarget.position - transform.position;
        targetDirection.y = 0; // Keep rotation strictly on the horizontal plane

        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void HandleMovement()
    {
        // Calculate distance strictly on the horizontal plane (X and Z) 
        // This stops VR headset vertical height variance from messing up the distance calculation
        Vector3 playerPosPlane = new Vector3(playerTarget.position.x, transform.position.y, playerTarget.position.z);
        float distanceToPlayer = Vector3.Distance(transform.position, playerPosPlane);
        
        Vector3 moveStep = Vector3.zero;

        // 1. Calculate horizontal step per frame
        if (distanceToPlayer > stoppingDistance)
        {
            moveStep = transform.forward * moveSpeed * Time.deltaTime;
        }

        // 2. Accumulate gravity velocity over time
        if (controller.isGrounded)
        {
            verticalVelocity = -2f; // Slight downward force to keep the controller firmly clamped to the ground
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        // 3. Apply the vertical step per frame (Velocity * Time) separately
        moveStep.y = verticalVelocity * Time.deltaTime;

        // 4. Feed the finalized per-frame step to the controller
        controller.Move(moveStep);
    }
}