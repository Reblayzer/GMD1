using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MoveJumpAbility : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private float movementX;
    private float movementY;
    private float speed = 0.15f;
    private float jumpForce = 7f;
    private int groundContactCount = 0; // Track number of ground contacts

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!OptionsMenu.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Space) && groundContactCount > 0)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
                groundContactCount = 0; // Reset to avoid double jumps
            }
        }
    }

    void FixedUpdate() 
    {
        if (Camera.main == null) return; // Ensure the camera exists

        // Get the camera's forward and right directions
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        // Ignore the Y component to keep movement horizontal
        cameraForward.y = 0;
        cameraRight.y = 0;

        // Normalize the vectors to prevent diagonal movement from being faster
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Convert input direction to world direction
        Vector3 movement = cameraRight * movementX + cameraForward * movementY;

        rb.AddForce(movement * speed, ForceMode.Impulse);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundContactCount++; // Increase count for each ground contact
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundContactCount = Mathf.Max(0, groundContactCount - 1); // Prevent negative values
        }
    }
}
