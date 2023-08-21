using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    [Header("Trialling components")]
    [SerializeField] private RectTransform button1;
    [SerializeField] private RectTransform button2;

    [SerializeField] private Slider x_pos;
    [SerializeField] private Slider y_pos;
    [SerializeField] private Slider scale;

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

    private void Update() {
        button1.localPosition = new Vector3(x_pos.value, y_pos.value, 1f);
        button1.localScale = Vector3.one * scale.value;

        button2.localPosition = new Vector3(-x_pos.value, y_pos.value, 1f);
        button2.localScale = Vector3.one * scale.value;
    }
}
