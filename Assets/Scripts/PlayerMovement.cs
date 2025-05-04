using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]float runSpeed = 10f;
    Vector2 moveInput;
    Rigidbody2D GumayushiRigidBody;

    void Start()
    {
        GumayushiRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Run();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }
    
    //y is set to deafult value as to give it gravity feel
    //We are adding new velocity each frame but for x only so y remains constant
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, GumayushiRigidBody.linearVelocity.y);
        GumayushiRigidBody.linearVelocity = playerVelocity;
    }
}
