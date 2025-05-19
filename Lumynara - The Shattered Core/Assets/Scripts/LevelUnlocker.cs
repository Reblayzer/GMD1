using UnityEngine;
using System.Collections.Generic;

public class LevelUnlocker : MonoBehaviour
{
  [Header("Drag your Level 1→5 buttons here (in order)")]
  [SerializeField] private List<GameObject> levelButtons;

  [Header("Drag your Final level button here")]
  [SerializeField] private GameObject finalLevelButton;

  private void Start()
  {
    // 1) Safely grab total shards (0 if no manager present)
    int totalShards = ShardPersistentManager.Instance != null
        ? ShardPersistentManager.Instance.GetTotalShardsCollected()
        : 0;

    // 2) Compute how many of the first five to unlock:
    //    0–9 → 1, 10–19 → 2, … 40–49 → 5
    int toShow = totalShards / 10 + 1;
    toShow = Mathf.Clamp(toShow, 1, levelButtons.Count);

    // 3) Activate the first N, deactivate the rest
    for (int i = 0; i < levelButtons.Count; i++)
      levelButtons[i].SetActive(i < toShow);

    // 4) Show “Final” only at 50+ shards
    if (finalLevelButton != null)
      finalLevelButton.SetActive(totalShards >= 50);
  }
}
