using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    /*
    need custom animation for button as settings menu will close
    if not selected (e.g. clicking button in settings menu will close it)
    having this custom menu code allows it to have better functionallity
    */ 

    // animator used to change bools
    [SerializeField] private Animator settingsAnimator;

    public void OnHover() {
        settingsAnimator.SetBool("isHovering", true);
    }

    public void OffHover() {
        settingsAnimator.SetBool("isHovering", false);
    }

    // on click (hamburger icon)
    public void OpenMenu() {
        settingsAnimator.SetBool("isOn", true);
    }

    // on click (close icon IN menu)
    public void CloseMenu() {
        settingsAnimator.SetBool("isOn", false);
    }
}