using TMPro;
using UnityEngine;

public class VFX : MonoBehaviour
{
    // text that need a colour change
    [SerializeField] private TextMeshProUGUI vfxText;

    // colours to change to
    [SerializeField] private Color vfxOnColour;
    [SerializeField] private Color vfxOffColour;

    // vfx objects to turn off
    [SerializeField] private GameObject[] vfxObjs;
    // true --> has vfx on
    // false --> has vfx off
    private bool vfxState = true;

    private void Start() {
        vfxText.color = vfxOnColour;
    }

    public void ToggleVFX() {
        // inverts vfx state when function is called
        // true --> false
        // false --> true
        vfxState = !vfxState;

        // loops through all given particles
        foreach (GameObject vfxObj in vfxObjs) {
            // stops particle systems
            vfxObj.SetActive(vfxState);
        }

        if (vfxState) {
            vfxText.color = vfxOnColour;
        }   else {
            vfxText.color = vfxOffColour;
        }
    }
}
