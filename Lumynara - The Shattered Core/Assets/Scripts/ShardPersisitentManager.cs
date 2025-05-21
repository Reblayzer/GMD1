using UnityEngine;
using System.Collections.Generic;

public class ShardPersistentManager : MonoBehaviour
{
    public static ShardPersistentManager Instance { get; private set; }

    // which levels have ever been beaten
    private HashSet<string> completedLevels = new HashSet<string>();

    // best final‐score per level
    private Dictionary<string, int> bestScorePerLevel = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    // Call once at end‐of‐level; marks level completed and records your finalScore.
    public void MarkLevelCompleted(string levelName, int finalScore)
    {
        completedLevels.Add(levelName);

        if (!bestScorePerLevel.ContainsKey(levelName) ||
            finalScore > bestScorePerLevel[levelName])
        {
            bestScorePerLevel[levelName] = finalScore;
        }
    }

    //True if levelName has ever been beaten.</summary>
    public bool IsLevelCompleted(string levelName)
        => completedLevels.Contains(levelName);

    //Returns that level’s best final‐score, or 0 if never beaten.</summary>
    public int GetBestScoreForLevel(string levelName)
        => bestScorePerLevel.TryGetValue(levelName, out var s) ? s : 0;

    //Total shards = 10 × number of completed levels.</summary>
    public int GetTotalShardsCollected()
        => completedLevels.Count * 10;
}
