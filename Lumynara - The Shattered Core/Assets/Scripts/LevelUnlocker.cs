using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUnlocker : MonoBehaviour
{
  [Header("Containers under Canvas")]
  [SerializeField] private Transform levelsContainer;     // holds your Level-1…Level-4 buttons
  [SerializeField] private Transform scoreContainer;      // holds your Level-1…Level-4 score Texts

  [Header("Final level button")]
  [SerializeField] private Button finalLevelButton;

  private Button[] levelButtons;
  private TMP_Text[] levelScoreTexts;

  private const int ShardsPerLevel = 10;

  private void Awake()
  {
    // grab them in hierarchy order (make sure your buttons/texts are lined up)
    levelButtons = levelsContainer.GetComponentsInChildren<Button>(true);
    levelScoreTexts = scoreContainer.GetComponentsInChildren<TMP_Text>(true);

    if (levelButtons.Length != levelScoreTexts.Length)
      Debug.LogError($"LevelUnlocker: {levelButtons.Length} buttons vs. {levelScoreTexts.Length} score texts", this);
  }

  private void Start()
  {
    var mgr = ShardPersistentManager.Instance;
    int total = mgr != null ? mgr.GetTotalShardsCollected() : 0;

    // how many of the first N levels to show: 0–9→1,10–19→2,…,30–39→4
    int toShow = Mathf.Clamp(total / ShardsPerLevel + 1, 1, levelButtons.Length);

    for (int i = 0; i < levelButtons.Length; i++)
    {
      string lvlName = $"Level {i + 1}";
      bool unlocked = i < toShow;
      bool completed = mgr != null && mgr.IsLevelCompleted(lvlName);

      // show/hide button
      levelButtons[i].gameObject.SetActive(unlocked);

      // show/hide score-text, and if unlocked, set its value
      levelScoreTexts[i].gameObject.SetActive(completed);
      if (completed)
        levelScoreTexts[i].text = mgr.GetBestScoreForLevel(lvlName).ToString();
    }

    // Final unlocks at 4 * 10 = 40 shards
    if (finalLevelButton != null)
      finalLevelButton.gameObject.SetActive(total >= levelButtons.Length * ShardsPerLevel);
  }
}
