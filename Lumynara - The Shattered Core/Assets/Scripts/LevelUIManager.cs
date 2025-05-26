using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class LevelUIManager : MonoBehaviour
{
  public static LevelUIManager Instance { get; private set; }

  public enum UIState { None, Paused, Completed, Shot, Fallen }

  [Header("Panels & Texts")]
  [SerializeField] private GameObject background;
  [SerializeField] private GameObject menuPanel;
  [SerializeField] private TMP_Text labelText;
  [SerializeField] private TMP_Text scoreText;
  [SerializeField] private TMP_Text timeText;
  [SerializeField] private Button primaryButton;
  [SerializeField] private TMP_Text primaryButtonText;
  [SerializeField] private Button restartButton;
  [SerializeField] private TMP_Text restartButtonText;

  [Header("Configs")]
  [SerializeField] private LevelMenuConfig pauseConfig;
  [SerializeField] private LevelMenuConfig completeConfig;
  [SerializeField] private LevelMenuConfig shotConfig;
  [SerializeField] private LevelMenuConfig fellConfig;
  [SerializeField] private LevelMenuConfig gameCompleteConfig;


  [Header("Input")]
  [SerializeField] private InputActionReference pauseActionRef;

  private UIState state = UIState.None;

  // running counters
  [HideInInspector] public int TotalShards = 40;
  private int shardsCollected;
  private int currentScore;
  private float levelTimer;

  public int CurrentScore => currentScore;

  private void Awake()
  {
    if (Instance == null) Instance = this;
    else { Destroy(gameObject); return; }

    primaryButton.onClick.AddListener(OnPrimaryButtonClicked);
    restartButton.onClick.AddListener(OnRestartButtonClicked);

    // start hidden & reset everything
    Resume();
    ResetCounters();

    Cursor.lockState = CursorLockMode.Locked;
  }

  private void OnEnable()
  {
    pauseActionRef.action.Enable();
    pauseActionRef.action.performed += OnPausePerformed;
  }

  private void OnDisable()
  {
    pauseActionRef.action.performed -= OnPausePerformed;
    pauseActionRef.action.Disable();
  }

  private void Update()
  {
    if (state == UIState.None)
    {
      levelTimer += Time.deltaTime;
      UpdateTimerUI();
    }
  }

  public void AddShard()
  {
    shardsCollected++;
  }

  public void RemoveShard()
  {
    shardsCollected = Mathf.Max(0, shardsCollected - 1);
  }

  private void ComputeFinalScore()
  {
    // e.g. 1000 pts per shard minus 10 pts per second
    currentScore = Mathf.Max(0, shardsCollected * 1000 - Mathf.FloorToInt(levelTimer) * 10);
    UpdateScoreUI();
  }

  public void ResetCounters()
  {
    shardsCollected = 0;
    currentScore = 0;
    levelTimer = 0f;
    UpdateScoreUI();
    UpdateTimerUI();
  }

  private void UpdateScoreUI()
  {
    if (scoreText != null)
      scoreText.text = $"Score: {currentScore}";
  }

  private void UpdateTimerUI()
  {
    if (timeText == null) return;
    int mins = (int)(levelTimer / 60f);
    int secs = (int)(levelTimer % 60f);
    timeText.text = $"Time: {mins:00}:{secs:00}";
  }

  private void ApplyConfig(in LevelMenuConfig cfg)
  {
    labelText.text = cfg.label;
    primaryButtonText.text = cfg.primaryText;
    restartButtonText.text = cfg.restartText;
  }

  private void ShowMenu(in LevelMenuConfig cfg, UIState newState)
  {
    state = newState;

    if (newState == UIState.Completed)
    {
      ComputeFinalScore();

      // persist this level + its final score
      ShardPersistentManager.Instance
          .MarkLevelCompleted(SceneManager.GetActiveScene().name, currentScore);
    }

    ApplyConfig(cfg);

    // only show the score text on Completed
    if (scoreText != null)
      scoreText.gameObject.SetActive(newState == UIState.Completed);

    background?.SetActive(true);
    menuPanel?.SetActive(true);

    Time.timeScale = 0f;
    AudioListener.pause = true;
    Cursor.lockState = CursorLockMode.None;
  }

  public void Resume()
  {
    state = UIState.None;

    background?.SetActive(false);
    menuPanel?.SetActive(false);

    // hide score during gameplay & other menus
    if (scoreText != null)
      scoreText.gameObject.SetActive(false);

    Time.timeScale = 1f;
    AudioListener.pause = false;
    Cursor.lockState = CursorLockMode.Locked;
  }

  public void ShowPause() => ShowMenu(pauseConfig, UIState.Paused);
  public void ShowLevelComplete() => ShowMenu(completeConfig, UIState.Completed);
  public void ShowHit() => ShowMenu(shotConfig, UIState.Shot);
  public void ShowFell() => ShowMenu(fellConfig, UIState.Fallen);

  private void OnPrimaryButtonClicked()
  {
    Resume();
    SceneManager.LoadSceneAsync("Levels Menu");
  }

  private void OnRestartButtonClicked()
  {
    Resume();
    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
  }

  private void OnPausePerformed(InputAction.CallbackContext ctx)
  {
    if (state == UIState.None) ShowPause();
    else Resume();
  }

  public void GetShardCounts(out int collected, out int total)
  {
    string sceneName = SceneManager.GetActiveScene().name;

    if (sceneName == "Level Final")
    {
      // In final level, Orbo starts with all shards
      collected = TotalShards;
      total = TotalShards;
    }
    else
    {
      // In other levels, we start with 0 collected
      collected = 0;
      total = GameObject.FindGameObjectsWithTag("Shard").Length;
    }
  }

  public void ShowGameCompleted()
  {
    state = UIState.Completed;

    ApplyConfig(gameCompleteConfig);

    if (scoreText != null) scoreText.gameObject.SetActive(false);
    if (timeText != null) timeText.gameObject.SetActive(false);
    if (restartButton != null) restartButton.gameObject.SetActive(false);

    background?.SetActive(true);
    menuPanel?.SetActive(true);

    Time.timeScale = 0f;
    AudioListener.pause = true;
    Cursor.lockState = CursorLockMode.None;
  }

}

[System.Serializable]
public struct LevelMenuConfig
{
  public string label;
  public string primaryText;
  public string restartText;
}