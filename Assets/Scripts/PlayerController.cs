
using UnityEngine; // this gives us access to monobehaviour, gameobject, transform, ridigbody, vector3, etc.
using UnityEngine.InputSystem; 
using TMPro;

public class PlayerController : MonoBehaviour
{

    public TMP_Text scoreDisplay;
    public float speed = 10.0f;

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
        UnityEngine.Vector2 movementVec = movementVal.Get<UnityEngine.Vector2>();

        movementX = movementVec.x;
        movementY = movementVec.y;
    }

    void FixedUpdate() //FIXED UPDATE FOR PHYSICS
    {
        
        UnityEngine.Vector3 movement = new UnityEngine.Vector3(movementX * speed, 0.0f, movementY * speed);
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
