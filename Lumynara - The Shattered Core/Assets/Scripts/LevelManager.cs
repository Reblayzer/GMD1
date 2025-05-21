using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void GoBack()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }

    public void playLevel(string levelNumber)
    {
        SceneManager.LoadSceneAsync(levelNumber);
    }
}
