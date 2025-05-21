using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CollisionManager : MonoBehaviour
{
    [Header("UI")]
    [Tooltip("Drop in your LevelUIManager (the one in the level scene)")]
    [SerializeField] private LevelUIManager uiManager;

    [Header("Shard Counter Display (in-level)")]
    [SerializeField] private TextMeshProUGUI countText;

    [Header("Portal")]
    [Tooltip("The child that you activate once all shards are collected")]
    [SerializeField] private GameObject portalChild;

    private int shardsCollectedInScene = 0;
    private int totalShardsInScene;

    private void Start()
    {
        // find how many shards exist when you started
        totalShardsInScene = GameObject.FindGameObjectsWithTag("Shard").Length;

        // hide portal until the last shard
        if (portalChild != null)
            portalChild.SetActive(false);

        UpdateCountUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shard"))
        {
            // 1) Hide the shard
            other.gameObject.SetActive(false);

            // 2) Tell LevelUIManager we got one
            if (LevelUIManager.Instance != null)
                LevelUIManager.Instance.AddShard();

            // keep your own count for the little corner display
            shardsCollectedInScene++;
            UpdateCountUI();

            // 3) If that was the last, show portal
            if (shardsCollectedInScene >= totalShardsInScene && portalChild != null)
                portalChild.SetActive(true);
        }
        else if (other.CompareTag("Portal"))
        {
            // only let them finish once it’s active
            if (portalChild != null && portalChild.activeInHierarchy)
            {
                uiManager.ShowLevelComplete();
            }
            else
            {
                Debug.Log("Portal locked—collect all shards first!");
            }
        }
    }

    private void UpdateCountUI()
    {
        if (countText != null)
            countText.text = $"{shardsCollectedInScene}/{totalShardsInScene}";
    }
}
