using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour
{
    [SerializeField] private GameObject[] VFXobjs;
    // true --> has VFX on
    // false --> has VFX off
    private bool VFXstate = true;

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
    }
}
