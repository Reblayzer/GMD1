using UnityEngine;
using UnityEngine.UI;

public class ToggleMuteController : MonoBehaviour
{
  [SerializeField] Toggle muteToggle;
  [SerializeField] HandleSelectable volumeHandle;
  [SerializeField] SliderLockable volumeSlider;

  void Awake()
  {
    muteToggle.onValueChanged.AddListener(OnToggle);
    OnToggle(muteToggle.isOn);
  }

  void OnToggle(bool isMuted)
  {
    volumeHandle.SetLocked(isMuted);
    if (volumeSlider != null)
      volumeSlider.isLocked = isMuted;
  }
}
