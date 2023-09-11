using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Animator settingsAnimator;

    public void OnHover() {
        settingsAnimator.SetBool("isHovering", true);
    }

    public void OffHover() {
        settingsAnimator.SetBool("isHovering", false);
    }

    public void OpenMenu() {
        settingsAnimator.SetBool("isOn", true);
    }

    public void CloseMenu() {
        settingsAnimator.SetBool("isOn", false);
    }
}