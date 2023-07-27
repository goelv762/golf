using UnityEngine;

public class ballController : MonoBehaviour {  

    [Header("Ball Components")]
    [SerializeField] private Rigidbody2D ballRB;
    [SerializeField] private float minPower, maxPower;
    [SerializeField] private float speedMulti;

    [Header("Input attributes")]
    [SerializeField] private float inputBuffer;

    [Header("Other")]
    [SerializeField] private Camera mainCamera;

    // stores mouse position
    private Vector2 shotPos;
    private Vector2 rawshotPos;

    // stores the value for the confirmation of shot
    private float shotActivate;
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

    // used to get inputs (e.g. mouse: pos, clicking)
    private UserInputs userInputs;
    private void Awake() {
        userInputs = new UserInputs();
    }

    private void OnEnable() {
        userInputs.Enable();
    }

    private void OnDisable() {
        userInputs.Disable();
    }

    private void Update() {
        GetInputs();

        shotVector = GetShotVector();

        if (shotActivate == 1f && isOverBall) { 
            isShotActive = true; 
        }

        if (shotActivate == 0f && !isOverBall && isShotActive) { 
            isShotActive = false;
            HitBall();
        }
        

        prevshotActivate = shotActivate;
    }

    void OnMouseEnter() { isOverBall = true; }
    void OnMouseExit() { isOverBall = false; }

    private void GetInputs() {
        rawshotPos = userInputs.User.shotPos.ReadValue<Vector2>();
        shotActivate = userInputs.User.shotActivate.ReadValue<float>();

        shotPos = mainCamera.ScreenToWorldPoint(rawshotPos);
    }

    private Vector2 GetShotVector() {
        Vector2 currShotVector;
        // get vector from ball pos to mouse pos
        currShotVector.x = shotPos.x - transform.position.x;
        currShotVector.y = shotPos.y - transform.position.y;

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
