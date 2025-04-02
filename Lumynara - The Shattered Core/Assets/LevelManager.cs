using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void GoBack() {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
