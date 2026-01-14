
using UnityEngine; // this gives us access to monobehaviour, gameobject, transform, ridigbody, vector3, etc.
using UnityEngine.InputSystem; 
using TMPro;

public class PlayerController : MonoBehaviour
{

    public TMP_Text scoreDisplay;
    public float speed = 10.0f;
    public Transform cam;
    public float jumpForce = 5.0f;

    private Rigidbody rb;
    private int PickupCnt = 0;

    private float movementX;
    private float movementY;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void OnJump(InputValue jumpVal) {

        if (!isGrounded) return;

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            PickupCnt++;
            SetScoreText();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void SetScoreText()
    {
        scoreDisplay.text = "Pickups Acquired: " + PickupCnt.ToString();
    }
}
