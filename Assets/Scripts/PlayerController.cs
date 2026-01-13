
using UnityEngine; // this gives us access to monobehaviour, gameobject, transform, ridigbody, vector3, etc.
using UnityEngine.InputSystem; 
using TMPro;

public class PlayerController : MonoBehaviour
{

    public TMP_Text scoreDisplay;
    public float speed = 10.0f;
    public Transform cam;

    private Rigidbody rb;
    private int PickupCnt = 0;

    private float movementX;
    private float movementY;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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

    void SetScoreText()
    {
        scoreDisplay.text = "Pickups Acquired: " + PickupCnt.ToString();
    }
}
