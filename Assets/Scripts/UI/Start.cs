using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
        public void LoadGame() {
        // next index of scenes in build settings
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
