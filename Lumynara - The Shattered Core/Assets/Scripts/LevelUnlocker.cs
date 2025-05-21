using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUnlocker : MonoBehaviour
{
  [Header("Containers under Canvas")]
  [SerializeField] private Transform levelsContainer;
  [SerializeField] private Transform scoreContainer;

  [Header("Final level button")]
  [SerializeField] private Button finalLevelButton;

  private Button[] levelButtons;
  private TMP_Text[] levelScoreTexts;

  private const int ShardsPerLevel = 10;

  private void Awake()
  {
    // populate arrays automatically
    levelButtons = levelsContainer.GetComponentsInChildren<Button>(true);
    levelScoreTexts = scoreContainer.GetComponentsInChildren<TMP_Text>(true);

    if (levelButtons.Length != levelScoreTexts.Length)
      Debug.LogError($"LevelUnlocker: {levelButtons.Length} buttons vs. {levelScoreTexts.Length} score texts", this);
  }

  private void Start()
  {
    var mgr = ShardPersistentManager.Instance;
    int total = mgr.GetTotalShardsCollected();

    // how many to unlock (1–5)
    int toShow = Mathf.Clamp(total / ShardsPerLevel + 1, 1, levelButtons.Length);

    for (int i = 0; i < levelButtons.Length; i++)
    {
      string levelName = $"Level {i + 1}";
      bool unlocked = i < toShow;
      bool completed = mgr.IsLevelCompleted(levelName);

      levelButtons[i].gameObject.SetActive(unlocked);
      levelScoreTexts[i].gameObject.SetActive(completed);

      if (completed)
      {
        // show that level’s best final‐score
        levelScoreTexts[i].text = mgr.GetBestScoreForLevel(levelName).ToString();
      }
    }

    // show Final button once you’ve racked up ≥50 shards
    if (finalLevelButton != null)
      finalLevelButton.gameObject.SetActive(total >= levelButtons.Length * ShardsPerLevel);
  }
}
