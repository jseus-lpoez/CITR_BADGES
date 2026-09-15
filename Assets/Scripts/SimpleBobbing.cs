using UnityEngine;

public class SimpleBobbing : MonoBehaviour
{
    public float bounceSpeed = 8.0f;
    public float bounceHeight = 0.1f;
    private float originalY;

    void Start()
    {
        originalY = transform.localPosition.y;
    }

    void Update()
    {
        // Create a perfect up-and-down sine wave loop
        float newY = originalY + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
    }
}