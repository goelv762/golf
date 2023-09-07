using TMPro;
using UnityEngine;

public class VFX : MonoBehaviour
{
    // text to be changed colour
    [SerializeField] private TextMeshProUGUI VFXtext;

    // colours to change to
    [SerializeField] private Color VFXon;
    [SerializeField] private Color VFXoff;

    [SerializeField] private GameObject[] VFXobjs;
    // true --> has VFX on
    // false --> has VFX off
    private bool VFXstate = true;

    private void Start() {
        VFXtext.color = VFXon;
    }

    public void ToggleVFX() {
        // inverts VFX state when function is called
        // true --> false
        // false --> true
        VFXstate = !VFXstate;

        // loops through all given particles
        foreach (GameObject VFXobj in VFXobjs) {
            // stops particle systems
            VFXobj.SetActive(VFXstate);
        }

        if (VFXstate) {
            VFXtext.color = VFXon;
        }   else {
            VFXtext.color = VFXoff;
        }
    }
}
