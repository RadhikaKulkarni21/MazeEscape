using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D benchRigidBody;

    void Start()
    {
        benchRigidBody = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        benchRigidBody.linearVelocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        moveSpeed = -moveSpeed;
        FlipEnemySprite();
    }

    void FlipEnemySprite()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(benchRigidBody.linearVelocity.x)), 1f);
    }
}
