using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]float runSpeed = 5f;
    [SerializeField]float jumpSpeed = 5f;

    Vector2 moveInput;
    Rigidbody2D gumayushiRigidBody;
    Animator gumayushiAnimator;
    CapsuleCollider2D gumayushiCollider;

    void Start()
    {
        gumayushiRigidBody = GetComponent<Rigidbody2D>();
        gumayushiAnimator = GetComponent<Animator>();
        gumayushiCollider = GetComponent<CapsuleCollider2D>();
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

    //Jump
    void OnJump(InputValue value)
    {
        //button pressed do a thing
        if (!gumayushiCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (value.isPressed)
        {
            gumayushiRigidBody.linearVelocity += new Vector2(0f, jumpSpeed);
        }
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
