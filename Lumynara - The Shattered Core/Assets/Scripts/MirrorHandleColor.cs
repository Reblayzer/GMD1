using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MirrorHandleColor : MonoBehaviour
{
  [Tooltip("Drag your Handle Image here")]
  public Image handleImage;

  private Image _fillImage;

  void Awake()
  {
    _fillImage = GetComponent<Image>();
  }

  void LateUpdate()
  {
    if (handleImage != null)
      _fillImage.color = handleImage.color;
  }
}
