using UnityEngine;
using System.Collections.Generic;

public class BallShot : MonoBehaviour
{

    [Header("Ball Components")]
    [SerializeField] private Rigidbody2D ballRB;
    [SerializeField] private int maxPower;
    [SerializeField] private float speedMulti;

    [Header("Input attributes")]
    [SerializeField] private float inputBuffer;

    [Header("Indicator")]
    [SerializeField] private GameObject arrowIndicator;
    [SerializeField] private float arrowIndicatorSpacing;

    [Header("Ball Colour")]
    [SerializeField] private Renderer ballRenderer;
    [SerializeField] Color hoverColour;
    [SerializeField] Color defaultColour;

    // stores the currentShotVector
    private Vector2 shotVector;

    // stores wether the mouse is above the ball
    private bool isOverBall;

    // stores weather there is a shot currently active (e.g. pulling back to shoot)
    private bool isShotActive;

    // stores shot length for speed
    private float shotVectorLength;

    // stores all the shot indicators
    private GameObject[] arrowIndicators;

    // stores the amount of currently active indicators
    private int currIndicatorActive;

    private void Awake() {
        // stores arrow indicators for later retrival
        // using _ as it is a unused var
        arrowIndicators = new GameObject[maxPower];

        for (int i = 0; i < arrowIndicators.Length; i++) {
            arrowIndicators[i] = Instantiate(arrowIndicator, transform.position, Quaternion.identity);

            // starts them hidden until needed
            arrowIndicators[i].SetActive(false);
        }
    }

    private void FixedUpdate() {
        BallHit();
        BallColour();
        ManageIndicator();
        shotVector = GetShotVector();
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
        else if (BallMaster.shotActivate == 0f && !isOverBall && isShotActive) { 
            isShotActive = false;

            if (shotVectorLength > maxPower) {
                ballRB.AddForce(-shotVector * maxPower * speedMulti, ForceMode2D.Impulse);
            } 
            
            else {
                ballRB.AddForce(-shotVector * shotVectorLength * speedMulti, ForceMode2D.Impulse);
            }

        } else if (BallMaster.shotActivate == 0f && isOverBall && isShotActive) {
            isShotActive = false;
        }
    }
    
    private void ManageIndicator() {

        if (isShotActive) {
            int targetIndicators = Mathf.RoundToInt(Mathf.Ceil(shotVectorLength));

            for (int k = 0; k < arrowIndicators.Length; k++) {
                // hides or shows the indicators based of shotVectorLength
                if (k < targetIndicators) {
                    Vector3 relativeIndicatorPos = Vector3.zero;
                    Vector2 currShotVector = shotVector * (k + 1) * arrowIndicatorSpacing;
                    // positions indicators if they are shown
                    relativeIndicatorPos = new Vector3(currShotVector.x, currShotVector.y, 0f);

                    // use negitave to get other side of ball 
                    arrowIndicators[k].transform.position = -relativeIndicatorPos + transform.position;
                    // use negitave to face opposite direction of ball
                    arrowIndicators[k].transform.up = -shotVector;
                    arrowIndicators[k].SetActive(true); 
                }
                else if (k >= targetIndicators) { 
                    arrowIndicators[k].SetActive(false); 
                } 
            }
        } 
        
        else {
            foreach (GameObject indicator in arrowIndicators) {
            indicator.SetActive(false);
            }
        }
    }

    private void BallColour() {
        if (isShotActive || isOverBall) {
            ballRenderer.material.color = hoverColour;
        } 
        else {
            ballRenderer.material.color = defaultColour;
        }
    }

    private Vector2 GetShotVector() {
        Vector2 currShotVector;
        // get vector from ball pos to mouse pos
        currShotVector.x = BallMaster.shotPos.x - transform.position.x;
        currShotVector.y = BallMaster.shotPos.y - transform.position.y;

        // pythagoras theorem (finding hypotenuse of triangle)
        // c = √(a^2 + b^2)
        // a = x, b = y
        shotVectorLength = Mathf.Sqrt(Mathf.Pow(currShotVector.x, 2) + Mathf.Pow(currShotVector.y, 2));

        // normalise the vector
        return currShotVector / shotVectorLength;
    }
}
