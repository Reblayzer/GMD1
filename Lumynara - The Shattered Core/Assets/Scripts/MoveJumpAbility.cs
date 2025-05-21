using UnityEngine;
using UnityEngine.InputSystem;

public class MoveJumpAbility : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float deceleration = 5f;
    [SerializeField] private float deadZone = 0.01f;
    [SerializeField] private float airControl = 0.3f;
    [SerializeField] private float jumpForce = 7f;

    private float movementX, movementY;
    private int groundContactCount;

    private void Awake()
    {
        // If you forgot to assign your Rigidbody in the inspector
        if (_rb == null) _rb = GetComponent<Rigidbody>();
    }

    // ← wire this to the PlayerInput “Move (Vector2)” UnityEvent
    public void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 v = ctx.ReadValue<Vector2>();
        movementX = v.x;
        movementY = v.y;
    }

    // ← wire this to the PlayerInput “Jump (Button)” UnityEvent
    public void OnJump(InputAction.CallbackContext ctx)
    {
        // only fire on a performed phase (not started or canceled)
        if (ctx.phase != InputActionPhase.Performed) return;

        // ignore if paused
        if (Time.timeScale == 0f) return;

        if (groundContactCount > 0)
        {
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            groundContactCount = 0;
        }
    }

    private void FixedUpdate()
    {
        if (Camera.main == null) return;

        // flatten camera axes
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = right.y = 0f;
        forward.Normalize();
        right.Normalize();

        var movement = right * movementX + forward * movementY;

        if (groundContactCount > 0)
        {
            // grounded
            if (movement.sqrMagnitude > deadZone)
            {
                var target = movement.normalized * speed;
                _rb.linearVelocity = new Vector3(target.x, _rb.linearVelocity.y, target.z);
            }
            else
            {
                var horiz = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);
                var decel = Vector3.Lerp(horiz, Vector3.zero, deceleration * Time.fixedDeltaTime);
                _rb.linearVelocity = new Vector3(decel.x, _rb.linearVelocity.y, decel.z);
            }
        }
        else if (movement.sqrMagnitude > deadZone)
        {
            // in air
            var horiz = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);
            var targetH = movement.normalized * speed;
            var newH = Vector3.MoveTowards(horiz, targetH, speed * airControl * Time.fixedDeltaTime);
            _rb.linearVelocity = new Vector3(newH.x, _rb.linearVelocity.y, newH.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            groundContactCount++;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            groundContactCount = Mathf.Max(0, groundContactCount - 1);
    }
}
