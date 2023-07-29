using UnityEngine;

public class BallMaster : MonoBehaviour {  
    [Header("Other")]
    [SerializeField] private Camera mainCamera;

    // stores mouse position
    static public Vector2 shotPos;
    static public Vector2 rawshotPos;

    // stores the value for the confirmation of shot
    static public float shotActivation;
    
    // used to get inputs (e.g. mouse: pos, clicking)
    private UserInputs userInputs;
    private void Awake() {
        userInputs = new UserInputs();
    }

    // enable / disable inputs
    private void OnEnable() { userInputs.Enable(); } 
    private void OnDisable() { userInputs.Disable(); }

    private void Update() {
        GetInputs();
    }

    private void GetInputs() {
        rawshotPos = userInputs.User.shotPos.ReadValue<Vector2>();
        shotActivation = userInputs.User.shotActivation.ReadValue<float>();

        shotPos = mainCamera.ScreenToWorldPoint(rawshotPos);
    }
}
