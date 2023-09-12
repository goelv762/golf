using UnityEngine.SceneManagement;
using UnityEngine;

public class Back : MonoBehaviour
{
    public void LastScene() {
        // loads current scene index - 1
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
