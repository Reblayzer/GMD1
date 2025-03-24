using UnityEngine;

public class MouseCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraTarget; // The player (sphere)
    [SerializeField] private Vector3 offset = new Vector3(0, 3, -5);
    [SerializeField] private float rotationSpeed = 3.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float minPitch = -30f;
    private float maxPitch = 50f;

    void Start()
    {
        if (cameraTarget != null)
        {
            Cursor.lockState = CursorLockMode.Locked;
            transform.position = cameraTarget.position + offset;
        }
    }

    void LateUpdate()
    {
        if (cameraTarget == null) return;

        // Mouse input
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;

        // Clamp pitch to avoid extreme angles
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Rotate only the camera view, not its position around the player
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desiredPosition = cameraTarget.position + rotation * offset;

        // Maintain correct distance from player
        transform.position = desiredPosition;
        transform.LookAt(cameraTarget.position);
    }
}
