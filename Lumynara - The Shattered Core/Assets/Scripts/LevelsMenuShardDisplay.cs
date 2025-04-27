using UnityEngine;
using TMPro;

public class LevelsMenuShardDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI shardCounterText;

    private void Start()
    {
        if (ShardPersistentManager.Instance != null)
        {
            int total = ShardPersistentManager.Instance.GetTotalShardsCollected();
            shardCounterText.text = total.ToString();
        }
    }
}
