using UnityEngine.SceneManagement;
using UnityEngine;

public class Back : MonoBehaviour
{
    public void ToMenu() {
        // loads menu scene index (always  first therefore index 0)
        SceneManager.LoadScene(0);
    }
}
