using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player; //  reference to player object
    private Vector3 offset; //  camera offset for player

    void Start()
    {
        offset = transform.position - player.transform.position;
    }
    
    void Update()
    {
        transform.position = offset + player.transform.position;
    }

}
