using UnityEngine;

public class BallShot : MonoBehaviour
{

    [Header("Ball Components")]
    [SerializeField] private Rigidbody2D ballRB;
    [SerializeField] private float minPower, maxPower;
    [SerializeField] private float speedMulti;

    [Header("Input attributes")]
    [SerializeField] private float inputBuffer;

    

    
    // stores last shotActivate val to make sure player cant hold down shot
    private float prevshotActivate;

    // stores wether the mouse is above the ball
    private bool isOverBall;

    // stores weather there is a shot currently active (e.g. pulling back to shoot)
    private bool isShotActive;


    // stores vector for shot vector
    private Vector2 shotVector;
    // stores shot length for speed
    private float shotVectorLength;
    // Update is called once per frame
    void Update() {
        shotVector = GetShotVector();

        if (BallMaster.shotActivate == 1f && isOverBall) { 
            isShotActive = true; 
        }

        if (BallMaster.shotActivate == 0f && !isOverBall && isShotActive) { 
            isShotActive = false;
            HitBall();
        }
        
        prevshotActivate = BallMaster.shotActivate;
    }

    void OnMouseEnter() { isOverBall = true; }
    void OnMouseExit() { isOverBall = false; }

    

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

    private void HitBall() {
        ballRB.AddForce(-shotVector * shotVectorLength * speedMulti, ForceMode2D.Impulse);
    }
}
