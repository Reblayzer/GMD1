using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MoveJumpAbility : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    private float movementX;
    private float movementY;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float deceleration = 5f;
    [SerializeField] private float deadZone = 0.01f;
    [SerializeField] private float airControl = 0.3f;
    private float jumpForce = 7f;
    private int groundContactCount = 0; // Track number of ground contacts

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!InGameOptionsMenu.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Space) && groundContactCount > 0)
            {
                _rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
                groundContactCount = 0; // Reset to avoid double jumps
            }
        }
    }

    void FixedUpdate()
    {
        if (Camera.main == null) return;

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 movement = right * movementX + forward * movementY;

        if (groundContactCount > 0)
        {
            if (movement.sqrMagnitude > deadZone)
            {
                Vector3 dir = movement.normalized;
                Vector3 targetVel = dir * speed;
                _rb.linearVelocity = new Vector3(targetVel.x, _rb.linearVelocity.y, targetVel.z);
            }
            else
            {
                Vector3 currentH = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);
                Vector3 decelH = Vector3.Lerp(currentH, Vector3.zero, deceleration * Time.fixedDeltaTime);
                _rb.linearVelocity = new Vector3(decelH.x, _rb.linearVelocity.y, decelH.z);
            }
        }
        else
        {
            if (movement.sqrMagnitude > deadZone)
            {
                Vector3 dir = movement.normalized;
                Vector3 currentH = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);
                Vector3 targetH = dir * speed;

                Vector3 newH = Vector3.MoveTowards(
                    currentH,
                    targetH,
                    speed * airControl * Time.fixedDeltaTime
                );
                _rb.linearVelocity = new Vector3(newH.x, _rb.linearVelocity.y, newH.z);
            }
        }
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
