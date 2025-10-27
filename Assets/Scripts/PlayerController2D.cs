using UnityEngine;
using UnityEngine.InputSystem;

//PlayerController2D
//Handles player movement (based on directional input using the InputSystem feature 
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D : MonoBehaviour
{

    [Header("Tuning")]
    public float moveSpeed = 5f;

    Rigidbody2D rb;
    Vector2 moveInput; //directional input

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //Input System callback: called by PlayerInput, Events, Movement
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput.normalized * moveSpeed; //implement physics based input
    }
}
