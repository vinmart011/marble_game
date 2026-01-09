using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    public GameObject player; //  reference to player object
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
        yaw += look.x * 100f * Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(0f, yaw, 0f);
        transform.position = player.transform.position + rotation * offset;
        transform.LookAt(player.transform.position);

    }

}
