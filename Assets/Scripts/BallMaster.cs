using UnityEngine;

public class BallMaster : MonoBehaviour {  
    [Header("Other")]
    [SerializeField] private Camera mainCamera;

    // stores mouse position
    static public Vector2 shotPos;
    static public Vector2 rawshotPos;

    // stores the value for the confirmation of shot
    static public float shotActivate;

    // stores the current shot vector
    static public Vector2 shotVector;

    // stores shot length for speed
    static public float shotVectorLength;

    // used to get inputs (e.g. mouse: pos, clicking)
    private UserInputs userInputs;
    private void Awake() {
        userInputs = new UserInputs();
    }

    private void OnEnable() { userInputs.Enable(); } 
    private void OnDisable() { userInputs.Disable(); }

    private void Update() {
        GetInputs();
        shotVector = GetShotVector();
    }

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
}
