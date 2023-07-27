using UnityEngine;

public class BallShot : MonoBehaviour
{

    [Header("Ball Components")]
    [SerializeField] private Rigidbody2D ballRB;
    [SerializeField] private float minPower, maxPower;
    [SerializeField] private float speedMulti;

    [Header("Input attributes")]
    [SerializeField] private float inputBuffer;

    [Header("Visual")]
    [SerializeField] private Renderer ballRenderer;
    [SerializeField] Color hoverColour;
    [SerializeField] Color defaultColour;

    // stores wether the mouse is above the ball
    private bool isOverBall;

    // stores weather there is a shot currently active (e.g. pulling back to shoot)
    private bool isShotActive;


    // stores vector for shot vector
    private Vector2 shotVector;
    // stores shot length for speed
    private float shotVectorLength;
    // Update is called once per frame
    private void Update() {
        shotVector = GetShotVector();

        if (CheckBallHit()) {
            ballRB.AddForce(-shotVector * shotVectorLength * speedMulti, ForceMode2D.Impulse);
        }

        BallColour();
    }

    private void OnMouseEnter() { isOverBall = true; }
    private void OnMouseExit() { isOverBall = false; }

    

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

    private bool CheckBallHit() {
        // logic for click and drag shooting
        // initial mouse down on ball
        if (BallMaster.shotActivate == 1f && isOverBall) { 
            isShotActive = true; 
        }

        // release
        if (BallMaster.shotActivate == 0f && !isOverBall && isShotActive) { 
            isShotActive = false;
            return true;
        }

        return false;
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
