using UnityEngine;

public class ballController : MonoBehaviour {  

    [Header("Ball Components")]
    [SerializeField] private Rigidbody2D ballRB;

    [Header("Ball attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float maxPower;

    [Header("Other")]
    [SerializeField] private Camera mainCamera;

    // stores mouse position
    private Vector2 shotDir;

    // stores vector for shot vector
    private Vector2 shotVector;

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
        getInputs();

        shotVector = getShotVector();

        Debug.Log(shotVector);
    }

    private void getInputs() {
        shotDir = mainCamera.ScreenToWorldPoint(userInputs.User.shotDir.ReadValue<Vector2>());
    }

    private Vector2 getShotVector() {
        float shotVectorLength;
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
}
