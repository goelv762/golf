using UnityEngine.SceneManagement;
using UnityEngine;

public class Back : MonoBehaviour
{
    public void LastScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
