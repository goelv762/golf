using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
        public void LoadGame() {
        // loads current scene index + 1
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
