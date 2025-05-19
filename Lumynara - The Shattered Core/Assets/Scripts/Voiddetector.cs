using UnityEngine;

[RequireComponent(typeof(Transform))]
public class VoidDetector : MonoBehaviour
{
  [SerializeField] private LevelUIManager uiManager;

  [SerializeField] private float fallThresholdY = -10f;

  bool hasFallen;

  void Update()
  {
    if (!hasFallen && transform.position.y <= fallThresholdY)
    {
      hasFallen = true;
      uiManager.ShowFell();
    }
  }
}
