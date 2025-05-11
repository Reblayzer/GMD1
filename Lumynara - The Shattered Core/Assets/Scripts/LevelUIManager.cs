using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUIManager : MonoBehaviour
{
  [System.Serializable]
  public struct MenuConfig
  {
    public string label;
    public string primaryText;
    public string restartText;
  }

  public enum UIState { None, Paused, Completed }

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
  [SerializeField] private MenuConfig pauseConfig;
  [SerializeField] private MenuConfig completeConfig;

  [Header("Input")]
  [SerializeField] private InputActionReference pauseActionRef;

  private UIState state = UIState.None;

  private void Awake()
  {
    background.SetActive(false);
    menuPanel.SetActive(false);
    Time.timeScale = 1f;
  }

  private void OnEnable()
  {
    pauseActionRef.action.Enable();
    pauseActionRef.action.performed += _ =>
    {
      if (state == UIState.None) ShowPause();
      else if (state == UIState.Paused) Resume();
    };
  }

  private void OnDisable()
  {
    pauseActionRef.action.performed -= _ => { };
    pauseActionRef.action.Disable();
  }

  private void ApplyConfig(in MenuConfig cfg)
  {
    labelText.text = cfg.label;
    primaryButtonText.text = cfg.primaryText;
    restartButtonText.text = cfg.restartText;
  }

  private void ShowMenu(in MenuConfig cfg, UIState newState)
  {
    state = newState;
    ApplyConfig(cfg);
    background.SetActive(true);
    menuPanel.SetActive(true);
    Time.timeScale = 0f;
    AudioListener.pause = true;
  }

  public void ShowPause()
      => ShowMenu(pauseConfig, UIState.Paused);

  public void ShowLevelComplete()
      => ShowMenu(completeConfig, UIState.Completed);

  public void Resume()
  {
    state = UIState.None;
    background.SetActive(false);
    menuPanel.SetActive(false);
    Time.timeScale = 1f;
    AudioListener.pause = false;
  }

  public void OnPrimaryButtonClicked()
  {
    SceneManager.LoadSceneAsync("LevelsMenu");
  }

  public void OnRestartButtonClicked()
  {
    SceneManager.LoadSceneAsync("L1");
  }
}
