using UnityEngine;
using System.Collections.Generic;

public class ShardPersistentManager : MonoBehaviour
{
    public static ShardPersistentManager Instance;

    // Save the best shards collected per level
    private Dictionary<string, int> bestShardsPerLevel = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TryUpdateBest(string levelName, int shardsCollected)
    {
        if (!bestShardsPerLevel.ContainsKey(levelName))
        {
            bestShardsPerLevel[levelName] = shardsCollected;
        }
        else
        {
            if (shardsCollected > bestShardsPerLevel[levelName])
            {
                bestShardsPerLevel[levelName] = shardsCollected;
            }
        }
    }

    public int GetTotalShardsCollected()
    {
        int total = 0;
        foreach (var kvp in bestShardsPerLevel)
        {
            total += kvp.Value;
        }
        return total;
    }
}
