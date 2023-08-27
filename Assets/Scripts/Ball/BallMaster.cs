using UnityEngine;

public class BallMaster : MonoBehaviour {  
    [Header("Other")]
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
        userInputs = new UserInputs();
    }

    // enable / disable inputs
    private void OnEnable() { userInputs.Enable(); } 
    private void OnDisable() { userInputs.Disable(); }

    private void Update() {
        GetInputs();    
    }

    private void GetInputs() {
        rawshotPos = userInputs.Game.shotPos.ReadValue<Vector2>();
        shotActivation = userInputs.Game.shotActivation.ReadValue<float>();

        // dividing by 120 returns a delta value of -1, 0 or 1
        scrollDelta = userInputs.Game.Zoom.ReadValue<float>() / 120;

        shotPos = mainCamera.ScreenToWorldPoint(rawshotPos);
    }
}
