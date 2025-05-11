using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private GameUIManager uiManager;
    private int count = 0;
    private int totalShards;

    void Start()
    {
        totalShards = GameObject.FindGameObjectsWithTag("Shard").Length;
        UpdateCountUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shard"))
        {
            other.gameObject.SetActive(false);
            count++;
            UpdateCountUI();
        }
        else if (other.CompareTag("Portal"))
        {
            if (ShardPersistentManager.Instance != null)
            {
                string sceneName = SceneManager.GetActiveScene().name;
                ShardPersistentManager.Instance.TryUpdateBest(sceneName, count);
            }
            uiManager.ShowLevelComplete();
        }
    }

    private void UpdateCountUI()
    {
        countText.text = $"{count}/{totalShards}";
    }
}
