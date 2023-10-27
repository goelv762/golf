using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject settings;
    
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.tag == "Flag") {
            winCanvas.SetActive(true);
            settings.SetActive(false);
        }
    }
}
