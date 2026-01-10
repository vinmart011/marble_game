using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    public GameObject player; //  reference to player object
    public float sensitivity = 50.0f;
    private Vector3 offset; //  camera offset for player
    private InputAction lookAction;
    private float yaw;

    void Start()
    {
        offset = transform.position - player.transform.position;
        Cursor.lockState = CursorLockMode.Locked;
        lookAction = InputSystem.actions.FindAction("Look");
    }
    
    void Update()
    {
        
        Vector2 look = lookAction.ReadValue<Vector2>();
        yaw += look.x * sensitivity * Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(0f, yaw, 0f); // our horizontal movement of the mouse and or the controller is mapped to the vector2 "look", we then 
                                                             // 'accumulate' the rotation of the horizontal movement instead of just resetting it every frame and finding the difference in the vectors (yaw). this is what stops the camera from snapping back to origin
                                                             // we have to map this 'yaw' into a quaternion to be compatible with a transform/position

        transform.position = player.transform.position + rotation * offset; // this generates the orbit of the camera (a bit unsure about how this works fundamentally still)
        transform.LookAt(player.transform.position); // locks orientation of the offset camera to the player model

    }

}
