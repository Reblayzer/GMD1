using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private LevelUIManager uiManager;
    private int count = 0;
    private int totalShards;
    [SerializeField] private GameObject portalChild;

    void Start()
    {
        // Count shards at startup
        totalShards = GameObject.FindGameObjectsWithTag("Shard").Length;

        // Hide the portal visual until all shards are collected
        if (portalChild != null)
            portalChild.SetActive(false);

        UpdateCountUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shard"))
        {
            // Collect shard
            other.gameObject.SetActive(false);
            count++;
            UpdateCountUI();

            // If that was the last one, show the portal child
            if (count == totalShards && portalChild != null)
                portalChild.SetActive(true);
        }
        else if (other.CompareTag("Portal"))
        {
            // Only allow level complete once portalChild is active (i.e. all shards collected)
            if (portalChild != null && portalChild.activeInHierarchy)
            {
                // Save best shard count
                var mgr = ShardPersistentManager.Instance;
                if (mgr != null)
                    mgr.TryUpdateBest(SceneManager.GetActiveScene().name, count);

                // Show the “level complete” UI
                uiManager.ShowLevelComplete();
            }
            else
            {
                // Optional: feedback that the portal isn't ready yet
                Debug.Log("Portal locked: collect all shards first!");
            }
        }
    }

    private void UpdateCountUI()
    {
        countText.text = $"{count}/{totalShards}";
    }
}
