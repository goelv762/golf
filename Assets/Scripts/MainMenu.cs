using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public void LoadGame() {
        // next index of scenes in build settings
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() {
        // for editor
        UnityEditor.EditorApplication.isPlaying = false;

        // for built game
        Application.Quit();
    }
}
