using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private RigidBody rb;

    private float movementX;
    private float movementY;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMove(InputValue movementVal)
    {
        Vector2 movementVec = movementVal.Get<Vector2>();

        movementX = movementVec.X;
        movementY = movementVec.Y;
    }

    void FixedUpdate() //FIXED UPDATE FOR PHYSICS
    {
        
    }
}
