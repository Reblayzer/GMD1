using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class ShardsManager : MonoBehaviour
{
    private int count;
    [SerializeField] private TextMeshProUGUI countText;
    private int totalShards;

    void Start()
    {
        count = 0;
        totalShards = GameObject.FindGameObjectsWithTag("Shard").Length;
        SetCountText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shard"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
        }
        else if (other.CompareTag("Portal"))
        {
            if (ShardPersistentManager.Instance != null)
            {
                string currentScene = SceneManager.GetActiveScene().name;
                ShardPersistentManager.Instance.TryUpdateBest(currentScene, count);
            }

            SceneManager.LoadScene("LevelsMenu");
        }
    }

    void SetCountText()
    {
        countText.text = $"{count.ToString()}/{totalShards}";
    }
}
