using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform mainCameraTransform;

    private void Start()
    {
        if (Camera.main != null)
        {
            mainCameraTransform = Camera.main.transform;
        }
    }

    private void LateUpdate()
    {
        if (mainCameraTransform != null)
        {
            // 1. Force the canvas to copy the exact looking angle of your camera lens
            // This guarantees the text is perfectly flat to your eyes, matching a standard game HUD
            transform.rotation = mainCameraTransform.rotation;
        }
    }
}