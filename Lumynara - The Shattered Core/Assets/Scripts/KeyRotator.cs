using UnityEngine;

public class KeyRotator : MonoBehaviour
{
  [Header("Rotation Speed (degrees/sec)")]
  [Tooltip("Speed around the local X axis")]
  [SerializeField] private float speedX = 0f;
  [Tooltip("Speed around the local Y axis")]
  [SerializeField] private float speedY = 45f;
  [Tooltip("Speed around the local Z axis")]
  [SerializeField] private float speedZ = 0f;

  void Update()
  {
    // Rotate in local space by (speedX, speedY, speedZ) each second
    transform.Rotate(
        new Vector3(speedX, speedY, speedZ) * Time.deltaTime,
        Space.Self
    );
  }
}
