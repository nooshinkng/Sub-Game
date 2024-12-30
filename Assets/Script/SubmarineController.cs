using UnityEngine;

public class SubmarineController : MonoBehaviour
{
    public float flapForce = 5f; // Upward force for flapping
    public float forwardSpeed = 2f; // Constant forward speed
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Constant forward movement
        rb.linearVelocity = new Vector3(forwardSpeed, rb.linearVelocity.y, 0);

        // Flap upwards on input
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, flapForce, 0);
        }
    }
    
}
