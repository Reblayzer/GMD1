using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public void GoBack() {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
