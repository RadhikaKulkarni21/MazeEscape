using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]float runSpeed = 5f;
    [SerializeField]float jumpSpeed = 5f;
    [SerializeField]float climbSpeed = 5f;

    Vector2 moveInput;
    Rigidbody2D gumayushiRigidBody;
    Animator gumayushiAnimator;
    CapsuleCollider2D gumayushiBodyCollider;
    BoxCollider2D gumayushiFeetCollider;
    float initialGravityScale;

    void Start()
    {
        gumayushiRigidBody = GetComponent<Rigidbody2D>();
        gumayushiAnimator = GetComponent<Animator>();
        gumayushiBodyCollider = GetComponent<CapsuleCollider2D>();
        gumayushiFeetCollider = GetComponent<BoxCollider2D>();
        initialGravityScale = gumayushiRigidBody.gravityScale;
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }


    //Greyed out fuctions are premade PlayerInput component, check the player and component
    //Greyed out because not called anywhere in the script but being called directly when player performs actions
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        //Debug.Log(moveInput);
    }

    //Jump
    void OnJump(InputValue value)
    {
        //button pressed do a thing
        if (!gumayushiFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (value.isPressed)
        {
            gumayushiRigidBody.linearVelocity += new Vector2(0f, jumpSpeed);
        }
    }
    
    //y is set to deafult value as to give it gravity feel
    //We are adding new velocity each frame but for x only so y should remain constant
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

    void ClimbLadder()
    {
        if (!gumayushiFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            gumayushiRigidBody.gravityScale = initialGravityScale;
            gumayushiAnimator.SetBool("IsClimbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2(gumayushiRigidBody.linearVelocity.x, moveInput.y * climbSpeed);
        gumayushiRigidBody.linearVelocity = climbVelocity;
        //To stop the player from drifting from the ladder
        gumayushiRigidBody.gravityScale = 0f;

        //Idle if not climbing
        bool playerHasVerticalSpeed = Mathf.Abs(gumayushiRigidBody.linearVelocity.y) > Mathf.Epsilon;
        gumayushiAnimator.SetBool("IsClimbing", playerHasVerticalSpeed);
    }
}
