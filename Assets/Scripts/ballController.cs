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
    private Vector2 shotDir;

    // stores the value for the confirmation of shot
    private float shotConfirm;
    // stores last shotConfirm val to make sure player cant hold down shot
    private float prevShotConfirm;


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

        // hitting ball
        if (shotConfirm > inputBuffer && prevShotConfirm != 1f) { 
            HitBall(); 
        }

        prevShotConfirm = shotConfirm;
    }

    private void GetInputs() {
        shotDir = mainCamera.ScreenToWorldPoint(userInputs.User.shotDir.ReadValue<Vector2>());
        shotConfirm = userInputs.User.shotConfirm.ReadValue<float>();
    }

    private Vector2 GetShotVector() {
        Vector2 currShotVector;
        // get vector from ball pos to mouse pos
        currShotVector.x = shotDir.x - transform.position.x;
        currShotVector.y = shotDir.y - transform.position.y;

        // pythagoras theorem (finding hypotenuse of triangle)
        // c = √(a^2 + b^2)
        // a = x, b = y
        shotVectorLength = Mathf.Sqrt(Mathf.Pow(currShotVector.x, 2) + Mathf.Pow(currShotVector.y, 2));

        // normalise the vector
        return currShotVector / shotVectorLength;
    }

    private void HitBall() {
        ballRB.AddForce(shotVector * shotVectorLength * speedMulti, ForceMode2D.Impulse);

    }
}
