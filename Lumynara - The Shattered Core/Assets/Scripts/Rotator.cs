using UnityEngine;

[AddComponentMenu("Utility/Rotator")]
public class Rotator : MonoBehaviour
{
  [Header("Rotation Speed (degrees per second)")]
  [Tooltip("Rotation speed around the X axis")]
  [SerializeField] private float speedX = 0f;

  [Tooltip("Rotation speed around the Y axis")]
  [SerializeField] private float speedY = 0f;

  [Tooltip("Rotation speed around the Z axis")]
  [SerializeField] private float speedZ = 0f;

  void Update()
  {
    // Compute rotation delta for this frame
    Vector3 delta = new Vector3(speedX, speedY, speedZ) * Time.deltaTime;
    // Apply rotation in world space
    transform.Rotate(delta, Space.Self);
  }
}