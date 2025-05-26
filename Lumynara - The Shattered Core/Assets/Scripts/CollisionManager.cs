using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

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
    [SerializeField] private GameObject shardCounterObject;
    [SerializeField] private GameObject visualShardPrefab;
    [SerializeField] private Transform orbo;
    [SerializeField] private Transform core;
    [SerializeField] private float arcHeight = 2f;


    private int shardsCollectedInScene = 0;
    private int totalShardsInScene;

    private void Start()
    {
        if (LevelUIManager.Instance != null)
        {
            LevelUIManager.Instance.GetShardCounts(out shardsCollectedInScene, out totalShardsInScene);
        }
        else
        {
            // Fallback: count objects with the tag
            totalShardsInScene = GameObject.FindGameObjectsWithTag("Shard").Length;
            shardsCollectedInScene = 0;
        }

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

            // 3) Local count
            shardsCollectedInScene++;
            UpdateCountUI();

            // 4) Show portal if all collected
            if (shardsCollectedInScene >= totalShardsInScene && portalChild != null)
                portalChild.SetActive(true);
        }
        else if (other.CompareTag("Portal"))
        {
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

    public IEnumerator TransferShardsToCore(float delayBetweenTransfers = 0.05f)
    {
        while (shardsCollectedInScene > 0)
        {
            shardsCollectedInScene--;
            totalShardsInScene--;

            if (LevelUIManager.Instance != null)
                LevelUIManager.Instance.RemoveShard();

            UpdateCountUI();

            // Spawn and animate visual shard
            if (visualShardPrefab != null && orbo != null && core != null)
            {
                Vector3 randomOffset = Random.insideUnitSphere * 0.1f;
                Vector3 spawnPosition = orbo.position + randomOffset;
                GameObject shard = Instantiate(visualShardPrefab, spawnPosition, Quaternion.identity);
                StartCoroutine(MoveShardInArc(shard.transform, orbo.position, core.position, arcHeight, 0.5f));
            }

            yield return new WaitForSeconds(delayBetweenTransfers);
        }

        if (shardCounterObject != null)
            shardCounterObject.SetActive(false);
    }

    private IEnumerator MoveShardInArc(Transform shard, Vector3 start, Vector3 end, float height, float duration)
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;

            // Parabolic arc calculation
            Vector3 currentPos = Vector3.Lerp(start, end, t);
            currentPos.y += height * 4f * (t - t * t); // parabolic height: h * 4t(1-t)

            shard.position = currentPos;

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        shard.position = end;
        Destroy(shard.gameObject);
    }

}
