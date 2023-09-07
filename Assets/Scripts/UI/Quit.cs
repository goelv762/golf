using UnityEngine;

public class Quit : MonoBehaviour
{
        public void QuitGame() {
        // for editor
# if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
# endif
        // for built game
        Application.Quit();
    }
}
