using UnityEngine;

public class BallMaster : MonoBehaviour {  
    [Header("Other")]
    // used to convert world and screen space
    [SerializeField] private Camera mainCamera;

    // stores mouse position
    static public Vector2 shotPos;
    static public Vector2 rawshotPos;

    // stores the value for the confirmation of shot
    static public float shotActivation;

    //  stores the value for scroll whell delta (only y axis)
    static public float scrollDelta;
    
    // used to get inputs (e.g. mouse: pos, clicking)
    private UserInputs userInputs;
    private void Awake() {
        // instance of user input
        userInputs = new UserInputs();
    }

    // enable / disable inputs
    private void OnEnable() { 
        // enables input
        userInputs.Enable(); 
    }

    private void OnDisable() { 
        // disables input
        userInputs.Disable(); 
    }

    private void Update() {
        GetInputs();    
    }

    private void GetInputs() {
        // mouse position used for getting shot position
        rawshotPos = userInputs.Game.shotPos.ReadValue<Vector2>();

        // mouse down used to activate shot
        shotActivation = userInputs.Game.shotActivation.ReadValue<float>();

        // dividing by 120 returns a delta value of -1, 0 or 1
        scrollDelta = userInputs.Game.Zoom.ReadValue<float>() / 120;

        // converts from screen to world points
        shotPos = mainCamera.ScreenToWorldPoint(rawshotPos);
    }
}
