using UnityEngine;

public class BallShot : MonoBehaviour {

    [Header("Ball Components")]

    // used to apply force to ball
    [SerializeField] private Rigidbody2D ballRB;

    // max amount of indicators that can appear
    [SerializeField] private int maxPower;

    // min speed before stopping
    [SerializeField] private float minSpeed;
    [SerializeField] private float speedMulti;

    [Header("Input attributes")]

    // minimum distance player can take a shot from
    [SerializeField] private float minShotDist;

    // how fast the ball can be travelling before the ball can eb shot again
    [SerializeField] private float shotMaxSpeed;

    [Header("Indicator")]

    // arrow indicator
    [SerializeField] private GameObject shotIndicator;

    // space between indicators
    [SerializeField] private float shotIndicatorSpacing;

    [Header("Ball Colour")]

    // used to change colour of ball
    [SerializeField] private Renderer ballRenderer;

    // colour on hover
    [SerializeField] Color hoverColour;

    // colour default
    [SerializeField] Color defaultColour;

    // stores the currentShotVector
    private Vector2 shotVector;
    // stored to make sure the indicator is not being updated for no reason
    private Vector2 prevShotVector;

    // stores weather a indicator is active
    private bool isIndicatorActive;

    // stores shot length for speed
    private float shotVectorLength;

    // stores if a shot is active
    private bool isShotActive;

    // if the cursor is over the ball
    private bool isOverBall;

    // stores all the shot indicators
    private GameObject[] shotIndicators;

    private void Awake() {
        // creates an array of type GameObject with len maxPower
        shotIndicators = new GameObject[maxPower];

        // loops through array
        for (int i = 0; i < shotIndicators.Length; i++) {

            // spawns indicators from prefab into gamObjects
            shotIndicators[i] = Instantiate(shotIndicator, transform.position, Quaternion.identity);

            // starts them hidden until needed
            shotIndicators[i].SetActive(false);
        }
    }

    private void OnMouseEnter() { 
        isOverBall = true; 
    }
    private void OnMouseExit() { 
        isOverBall = false; 
    }

    private void FixedUpdate() {
        // for hitting the ball
        BallHit();
    }

    private void Update() {
        // for changing the colour of ball
        BallColour();

        // showing and hiding the shot indicator
        ManageIndicator();

        // gets the normalised relitave vector of the shot
        shotVector = GetShotVector();

        // checks if velocity  < min speed --> stop ball
        if (ballRB.velocity.magnitude <= minSpeed) {
            ballRB.velocity = Vector2.zero;
        }
    }


    private void BallHit() {
        // logic for click and drag shooting
        // initial mouse down on ball
        if (BallMaster.shotActivation == 1f && isOverBall && ballRB.velocity.magnitude <= shotMaxSpeed) {
            isShotActive = true; 
        }

        // if the mouse HAS been released, shot is active and indiactor is active --> shoot
        else if (BallMaster.shotActivation == 0f && isShotActive && isIndicatorActive) { 
            isShotActive = false;

            // if shot power exeeds maxPower
            if (shotVectorLength > maxPower) {
                // hit ball with max force allowed
                ballRB.AddForce(-shotVector * maxPower * speedMulti, ForceMode2D.Impulse);
            }
            
            // if it doesn't
            else {
                // hit ball with current force
                ballRB.AddForce(-shotVector * shotVectorLength * speedMulti, ForceMode2D.Impulse);
            }

        // if the mouse HAS been released but is over ball --> cancel shot
        } else if (BallMaster.shotActivation == 0f && isOverBall && isShotActive) {
            isShotActive = false;
        }
    }


    private void ManageIndicator() {
        // if there is a shot active and not too small of a shot
        if (isShotActive && (shotVectorLength > minShotDist) && prevShotVector != shotVector) {

            // how many indicators need to be shown
            int targetIndicators = Mathf.RoundToInt(Mathf.Ceil(shotVectorLength));

            // loops through all indicators
            for (int k = 0; k < shotIndicators.Length; k++) {
                // hides or shows the indicators based of shotVectorLength
                // if it needs to be shown (< target) --> update position, set to visible
                if (k < targetIndicators) {
                    // finds spacing required using follow variables
                    Vector2 currShotVector = shotVector * (k + 1) * shotIndicatorSpacing;

                    // positions indicators if they are shown
                    Vector3 relativeIndicatorPos = new Vector3(currShotVector.x, currShotVector.y, 0f);

                    // use negitave to get other side of ball 
                    shotIndicators[k].transform.position = -relativeIndicatorPos + transform.position;

                    // use negitave to face opposite direction of ball
                    shotIndicators[k].transform.up = -shotVector;

                    // sets to visible (active)
                    shotIndicators[k].SetActive(true);

                    // sets bool to true to make sure list is not being looped through for no reason
                    isIndicatorActive = true;

                    

                }
                // if it does not need to be shown (>= target) --> don't update position, hide it
                else if (k >= targetIndicators) { 
                    // sets to invisible (inactive)
                    shotIndicators[k].SetActive(false);
                } 
            }

            // sets prev to current for future checks (next frame / tick)
            prevShotVector = shotVector;
        } 

        // checks if there is not an active shot and the indicator is still showing
        else if ((!isShotActive || (shotVectorLength <= minShotDist)) && isIndicatorActive) {

            // loops through all indicators
            for (int l = 0; l < shotIndicators.Length; l++) {

                // deactivates then as they are not being used
                shotIndicators[l].SetActive(false);
            }
            isIndicatorActive = false;
        }
    }

    private void BallColour() {
        // if there is an active shot or the mouse is over the ball --> change colour
        if ((isShotActive || isOverBall) && ballRB.velocity.magnitude <= shotMaxSpeed) {
            // change to hover colour
            ballRenderer.material.color = hoverColour;
        }
        // otherwise, set to default
        else {
            // change to default colour
            ballRenderer.material.color = defaultColour;
        }
    }

    private Vector2 GetShotVector() {
        Vector2 currShotVector = Vector2.zero;
        // get vector from ball pos to mouse pos
        // relative vector freom ball to mouse (shotPos) 
        currShotVector.x = BallMaster.shotPos.x - transform.position.x;
        currShotVector.y = BallMaster.shotPos.y - transform.position.y;

        // pythagoras theorem (finding hypotenuse of triangle), used to normalise vector
        // c = √(a^2 + b^2)
        // a = x, b = y
        shotVectorLength = Mathf.Sqrt(Mathf.Pow(currShotVector.x, 2) + Mathf.Pow(currShotVector.y, 2));

        // return the normalised the vector by dividing by length
        return currShotVector / shotVectorLength;
    }
}
