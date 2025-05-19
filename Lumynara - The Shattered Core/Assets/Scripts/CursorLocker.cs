using UnityEngine;

[AddComponentMenu("Utility/CursorLocker")]
public class CursorLocker : MonoBehaviour
{
  void Awake()
  {
    HideAndLock();
  }

  // In case the player alt-tabs back in:
  void OnApplicationFocus(bool hasFocus)
  {
    if (hasFocus)
      HideAndLock();
  }

  private void HideAndLock()
  {
    Cursor.visible = false;
    Cursor.lockState = CursorLockMode.Locked;
  }
}
