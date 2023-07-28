using UnityEngine;
using System.Collections.Generic;

public class BallShot : MonoBehaviour
{

    [Header("Ball Components")]
    [SerializeField] private Rigidbody2D ballRB;
    [SerializeField] private float minPower, maxPower;
    [SerializeField] private float speedMulti;

    [Header("Input attributes")]
    [SerializeField] private float inputBuffer;

    [Header("Visual")]
    [SerializeField] private GameObject arrowIndicator;
    [SerializeField] private Renderer ballRenderer;
    [SerializeField] Color hoverColour;
    [SerializeField] Color defaultColour;

    // stores wether the mouse is above the ball
    private bool isOverBall;

    // stores weather there is a shot currently active (e.g. pulling back to shoot)
    private bool isShotActive;

    // stores all the shot indicators
    private List<GameObject> arrowIndicators = new List<GameObject>();


    // Update is called once per frame
    private void Update() {

        BallHit();
        BallColour();
    }

    private void OnMouseEnter() { isOverBall = true; }
    private void OnMouseExit() { isOverBall = false; }

    private void BallHit() {
        // logic for click and drag shooting
        // initial mouse down on ball
        if (BallMaster.shotActivate == 1f && isOverBall) { 
            isShotActive = true; 
        }

        // release
        if (BallMaster.shotActivate == 0f && !isOverBall && isShotActive) { 
            isShotActive = false;
            ballRB.AddForce(-BallMaster.shotVector * BallMaster.shotVectorLength * speedMulti, ForceMode2D.Impulse);
            ShotIndicator();
        }
    }
    
    private void ShotIndicator() {
        
        int arraySize = arrowIndicators.Count;

        if (arraySize != Mathf.Round(BallMaster.shotVectorLength)) {
            for (int i = 0; i < arraySize; i++) {
                Debug.Log(i);
                arrowIndicators.Add(Instantiate(arrowIndicator));
            }
            // Debug.Log(Mathf.Round(BallMaster.shotVectorLength) + " | " + arraySize);
        }

        
        // for (int i = 0; i < arraySize; i++) {
        //     arrowIndicators[i].transform.position = transform.position;
        // }
    }

    private void BallColour() {
        if (isShotActive || isOverBall) {
            ballRenderer.material.color = hoverColour;
        } 
        else {
            ballRenderer.material.color = defaultColour;
        }
    }
}
