using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(2.45f, 5f, 0f);
    public Vector3 rotationEuler = new Vector3(45f, -90f, 0f);

    private bool isTransitioning = false;
    private bool finalCameraLocked = false;
    private Vector3 transitionStartPosition;
    private Quaternion transitionStartRotation;

    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private float transitionTimer = 0f;
    private float transitionDuration;

    void LateUpdate()
    {
        if (finalCameraLocked)
            return;

        if (isTransitioning)
        {
            transitionTimer += Time.deltaTime;
            float t = Mathf.Clamp01(transitionTimer / transitionDuration);

            transform.position = Vector3.Lerp(transitionStartPosition, targetPosition, t);
            transform.rotation = Quaternion.Lerp(transitionStartRotation, targetRotation, t);

            if (t >= 1f)
            {
                isTransitioning = false;
                finalCameraLocked = true;
            }

            return;
        }

        // ✅ Guard against null or destroyed player
        if (player == null)
            return;

        transform.position = player.position + offset;
        transform.rotation = Quaternion.Euler(rotationEuler);
    }

    public void StartCameraTransition(Vector3 newPos, Vector3 newEulerAngles, float duration)
    {
        transitionStartPosition = transform.position;
        transitionStartRotation = transform.rotation;

        targetPosition = newPos;
        targetRotation = Quaternion.Euler(newEulerAngles);

        transitionDuration = duration;
        transitionTimer = 0f;
        isTransitioning = true;
    }
}
