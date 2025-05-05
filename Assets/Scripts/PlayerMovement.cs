using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]float runSpeed = 10f;
    Vector2 moveInput;
    Rigidbody2D gumayushiRigidBody;
    Animator gumayushiAnimator;

    void Start()
    {
        gumayushiRigidBody = GetComponent<Rigidbody2D>();
        gumayushiAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
        FlipSprite();
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
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, gumayushiRigidBody.linearVelocity.y);
        gumayushiRigidBody.linearVelocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(gumayushiRigidBody.linearVelocity.x) > Mathf.Epsilon;

        gumayushiAnimator.SetBool("IsRunning", playerHasHorizontalSpeed);
    }

    //Flippinhg the character to go left when going left or right when going right
    void FlipSprite()
    {
        //adding a bool so that player moving left does not face right again when let go of the key
        bool playerHasHorizontalSpeed = Mathf.Abs(gumayushiRigidBody.linearVelocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(gumayushiRigidBody.linearVelocity.x), 1f);
        }      
    }
}
