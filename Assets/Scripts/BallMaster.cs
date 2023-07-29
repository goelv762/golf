using UnityEngine;

public class BallMaster : MonoBehaviour {  
    [Header("Other")]
    [SerializeField] private Camera mainCamera;

    // stores mouse position
    public static Vector2 shotPos;
    public static Vector2 rawshotPos;

    // stores shot length for speed
    public static float shotVectorLength;

    // stores the value for the confirmation of shot
    public static float shotActivation;

    // stores weather a shot is active or not
    public static bool isShotActive;

    // stores wether the mouse is above the ball
    public static bool isOverBall;
    
    // used to get inputs (e.g. mouse: pos, clicking)
    private UserInputs userInputs;

    private void Awake() {
        userInputs = new UserInputs();
    }

    // enable / disable inputs
    private void OnEnable() { userInputs.Enable(); } 
    private void OnDisable() { userInputs.Disable(); }

    // checking if mouse is over ball
    private void OnMouseEnter() { isOverBall = true; }
    private void OnMouseExit() { isOverBall = false; }

    private void Update() {
        GetInputs();
    }

    private void GetInputs() {
        rawshotPos = userInputs.User.shotPos.ReadValue<Vector2>();
        shotActivation = userInputs.User.shotActivation.ReadValue<float>();

        shotPos = mainCamera.ScreenToWorldPoint(rawshotPos);
    }

    public static Vector2 GetShotVector(Vector3 ballPos) {
        Vector2 currShotVector;
        // get vector from ball pos to mouse pos
        // relative vector freom ball to mouse (shotPos) 
        currShotVector.x = shotPos.x - ballPos.x;
        currShotVector.y = shotPos.y - ballPos.y;

        // pythagoras theorem (finding hypotenuse of triangle), used to normalise vector
        // c = √(a^2 + b^2)
        // a = x, b = y
        shotVectorLength = Mathf.Sqrt(Mathf.Pow(currShotVector.x, 2) + Mathf.Pow(currShotVector.y, 2));

        // return the normalised the vector by dividing by length
        return currShotVector / shotVectorLength;
    }
}
