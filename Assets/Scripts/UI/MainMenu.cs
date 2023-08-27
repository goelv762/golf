using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void LoadGame() {
        // next index of scenes in build settings
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() {
        // for editor
# if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
# endif
        // for built game
        Application.Quit();
    }
}
