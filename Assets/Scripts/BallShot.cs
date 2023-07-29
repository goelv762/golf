using UnityEngine;

public class BallShot : MonoBehaviour
{

    [Header("Ball Components")]
    [SerializeField] private Rigidbody2D ballRB;
    [SerializeField] private int maxPower;
    [SerializeField] private float speedMulti;

    [Header("Input attributes")]
    [SerializeField] private float inputBuffer;

    [Header("Indicator")]
    [SerializeField] private GameObject arrowIndicator;
    [SerializeField] private float arrowIndicatorSpacing;
    [SerializeField] private float minIndicatorDistance;

    [Header("Ball Colour")]
    [SerializeField] private Renderer ballRenderer;
    [SerializeField] Color hoverColour;
    [SerializeField] Color defaultColour;

    // stores the currentShotVector
    private Vector2 shotVector;

    // stores wether the mouse is above the ball
    private bool isOverBall;

    // stores weather there is a shot currently active (e.g. pulling back to shoot)
    private bool isShotActive;

    // stores weather a indicator is active
    private bool isIndicatorActive;

    // stores all the shot indicators
    private GameObject[] arrowIndicators;

    // stores the amount of currently active indicators
    private int currIndicatorActive;

    private void Awake() {
        // stores arrow indicators for later retrival
        // using _ as it is a unused var
        arrowIndicators = new GameObject[maxPower];

        for (int i = 0; i < arrowIndicators.Length; i++) {
            arrowIndicators[i] = Instantiate(arrowIndicator, transform.position, Quaternion.identity);

            // starts them hidden until needed
            arrowIndicators[i].SetActive(false);
        }
    }

    private void FixedUpdate() {
        // for hitting the ball
        BallHit();

        // for changing the colour of ball
        BallColour();

        // showing and hiding the shot indicator
        ManageIndicator();

        // gets the normalised relitave vector of the shot
        shotVector = BallMaster.GetShotVector(transform.position);
    }

    // when mouse is over ball
    private void OnMouseEnter() { isOverBall = true; }

    // when mouse moved away from ball / not on ball
    private void OnMouseExit() { isOverBall = false; }

    private void BallHit() {
        // logic for click and drag shooting
        // initial mouse down on ball
        if (BallMaster.shotActivate == 1f && isOverBall) { 
            isShotActive = true; 
        }

        // if the mouse HAS been released and is not over ball --> shoot
        else if (BallMaster.shotActivate == 0f && !isOverBall && isShotActive) { 
            isShotActive = false;

            // if shot power exeeds maxPower
            if (BallMaster.shotVectorLength > maxPower) {
                ballRB.AddForce(-shotVector * maxPower * speedMulti, ForceMode2D.Impulse);
            } 
            
            // if it doesn't
            else {
                ballRB.AddForce(-shotVector * BallMaster.shotVectorLength * speedMulti, ForceMode2D.Impulse);
            }

        // if the mouse HAS been released but is over ball --> cancel shot
        } else if (BallMaster.shotActivate == 0f && isOverBall && isShotActive) {
            isShotActive = false;
        }
    }
    
    private void ManageIndicator() {
        // if there is a shot active and not too small of a shot
        if (isShotActive && (BallMaster.shotVectorLength > minIndicatorDistance)) {

            // how many indicators need to be shown
            int targetIndicators = Mathf.RoundToInt(Mathf.Ceil(BallMaster.shotVectorLength));

            // loops through all indicators
            for (int k = 0; k < arrowIndicators.Length; k++) {
                // hides or shows the indicators based of shotVectorLength
                // if it needs to be shown (< target) --> update position, set to visible
                if (k < targetIndicators) {
                    Vector3 relativeIndicatorPos = Vector3.zero;
                    Vector2 currShotVector = shotVector * (k + 1) * arrowIndicatorSpacing;
                    // positions indicators if they are shown
                    relativeIndicatorPos = new Vector3(currShotVector.x, currShotVector.y, 0f);

                    // use negitave to get other side of ball 
                    arrowIndicators[k].transform.position = -relativeIndicatorPos + transform.position;

                    // use negitave to face opposite direction of ball
                    arrowIndicators[k].transform.up = -shotVector;

                    // sets to visible (active)
                    arrowIndicators[k].SetActive(true);

                    // sets bool to true to make sure list is not being looped through for no reason
                    isIndicatorActive = true;
                }
                // if it does not need to be shown (>= target) --> don't update position, hide it
                else if (k >= targetIndicators) { 
                    // sets to invisible (inactive)
                    arrowIndicators[k].SetActive(false); 

                    // sets bool to false to make sure list is not being looped through for no reason
                    isIndicatorActive = false;
                } 
            }
        } 

        // checks if there is not an active shot and the indicator is still showing
        else if (!isShotActive && isIndicatorActive) {

            // loops through all indicators
            foreach (GameObject indicator in arrowIndicators) {

                // sets to invisible (inactive)
                indicator.SetActive(false);
            }
        }
    }

    private void BallColour() {
        // if there is an active shot or the mouse is over the ball --> change colour
        if (isShotActive || isOverBall) {
            ballRenderer.material.color = hoverColour;
        } 
        // otherwise, set to default
        else {
            ballRenderer.material.color = defaultColour;
        }
    }

    
}
