using UnityEngine;

[AddComponentMenu("Movement/PlatformMover")]
public class PlatformMover : MonoBehaviour
{
  [Header("Offset from Initial Position (World Units)")]
  [Tooltip("Minimum X offset from the starting spot")][SerializeField] private float offsetMinX = -3f;
  [Tooltip("Maximum X offset from the starting spot")][SerializeField] private float offsetMaxX = 3f;

  [Tooltip("Minimum Y offset from the starting spot")][SerializeField] private float offsetMinY = 0f;
  [Tooltip("Maximum Y offset from the starting spot")][SerializeField] private float offsetMaxY = 0f;

  [Tooltip("Minimum Z offset from the starting spot")][SerializeField] private float offsetMinZ = -3f;
  [Tooltip("Maximum Z offset from the starting spot")][SerializeField] private float offsetMaxZ = 3f;

  [Header("Motion")]
  [Tooltip("Speed along the path (units per second)")][SerializeField] private float speed = 2f;

  // Internals
  private Vector3 _initialPos;
  private Vector3 _startPos;
  private Vector3 _endPos;
  private Vector3 _direction;
  private float _pathLength;

  void Awake()
  {
    // Record the platform's initial placement
    _initialPos = transform.position;

    // Compute world-space endpoints using offsets
    _startPos = new Vector3(
        _initialPos.x + offsetMinX,
        _initialPos.y + offsetMinY,
        _initialPos.z + offsetMinZ
    );
    _endPos = new Vector3(
        _initialPos.x + offsetMaxX,
        _initialPos.y + offsetMaxY,
        _initialPos.z + offsetMaxZ
    );

    // Precompute direction and path length
    Vector3 delta = _endPos - _startPos;
    _pathLength = delta.magnitude;
    _direction = (_pathLength > 0f) ? (delta / _pathLength) : Vector3.zero;
  }

  void Update()
  {
    // Only run motion in Play Mode and if there's a non-zero path
    if (!Application.isPlaying || _pathLength <= 0f)
      return;

    // Calculate distance along the path (PingPong cycles between 0 and _pathLength)
    float dist = Mathf.PingPong(Time.time * speed, _pathLength);
    transform.position = _startPos + _direction * dist;
  }

  void OnDrawGizmosSelected()
  {
    // Visualize the endpoints and path in the Editor
    Gizmos.color = Color.cyan;
    Vector3 basePos = (Application.isPlaying ? _initialPos : transform.position);

    Vector3 a = new Vector3(
        basePos.x + offsetMinX,
        basePos.y + offsetMinY,
        basePos.z + offsetMinZ
    );
    Vector3 b = new Vector3(
        basePos.x + offsetMaxX,
        basePos.y + offsetMaxY,
        basePos.z + offsetMaxZ
    );

    Gizmos.DrawWireSphere(a, 0.15f);
    Gizmos.DrawWireSphere(b, 0.15f);
    Gizmos.DrawLine(a, b);
  }
}