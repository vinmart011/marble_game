
using UnityEngine; // this gives us access to monobehaviour, gameobject, transform, ridigbody, vector3, etc.
using UnityEngine.InputSystem; //gives us access to player input controls
using TMPro;

public class PlayerController : MonoBehaviour
{

    public TMP_Text scoreDisplay; //reference to score display
    public float speed = 10.0f; //player speed
    public Transform cam; // reference to camera object for player movement relative to camera
    public float jumpForce = 5.0f; // multiplier for upwards jump movement
    public float fallMult = 2.5f; // fall multiplier for game feel

    private Rigidbody rb;
    private int PickupCnt = 0;

    private float movementX;
    private float movementY;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); //gets reference to player rigid body on game start so it isnt continually called
    }


    void OnJump(InputValue jumpVal) {

        if (!isGrounded) return; // makes sure ball is grounded before enabling jump again using tags

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // [0, 1, 0] * 5.0f = [0, 5.0, 0] this uses impulse which is a one off event
        isGrounded = false;

    }

    void OnMove(InputValue movementVal)
    {

        // TODO: make the ball accelerate to a top speed but lose speed when letting go 
        // of forward input or colliding with a barrier.

        UnityEngine.Vector2 movementVec = movementVal.Get<UnityEngine.Vector2>();
        movementX = movementVec.x;
        movementY = movementVec.y;
    }

    void FixedUpdate() //FIXED UPDATE FOR PHYSICS
    {

        float horInput = movementX * speed;
        float verInput = movementY * speed;


        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        camForward.y = 0;
        camRight.y = 0;

        Vector3 forwardRelative = verInput * camForward;
        Vector3 rightRelative = horInput * camRight;

        Vector3 moveDir = forwardRelative + rightRelative;

        UnityEngine.Vector3 movement = new UnityEngine.Vector3(moveDir.x, 0.0f, moveDir.z);
        rb.AddForce(movement);

        if (rb.linearVelocity.y < 0) // add extra downwards gravity after apex of jump for better feel
        {
            rb.AddForce(Vector3.up * Physics.gravity.y * (fallMult - 1), ForceMode.Acceleration);
        }

    }

    void OnTriggerEnter(Collider other) // function to check pickup tags in order to count players current pickups and despawn pickups
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            PickupCnt++;
            SetScoreText();
        }
    }

    void OnCollisionEnter(Collision collision) // set true if touching ground
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision) //set true if not touching ground i.e. jumping, falling, etc.
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void SetScoreText() // function to display current pickups
    {
        scoreDisplay.text = "Pickups Acquired: " + PickupCnt.ToString();
    }
}
