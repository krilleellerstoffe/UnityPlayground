using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private float dirX;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private LayerMask jumpableGround;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private enum MoveState
    {
        idling,
        running,
        jumping,
        falling
    }
     

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        body.velocity = new Vector2(dirX * moveSpeed, body.velocity.y);
        if (Input.GetButtonDown("Jump") && IsOnGround())
        {
            body.velocity = new Vector2(body.velocity.x, jumpForce);
        }
        UpdateAnimationState();
    }
    private void UpdateAnimationState()
    {
        MoveState moveState;

        if(dirX > 0f)
        {
            spriteRenderer.flipX = false;
            moveState = MoveState.running;
        }
        else if(dirX < 0f)
        {
            spriteRenderer.flipX = true;
            moveState = MoveState.running;
        }
        else
        {
            moveState = MoveState.idling;
        }
        if(body.velocity.y > 0.1f) 
        {
            moveState = MoveState.jumping;
        }
        else if (body.velocity.y < -0.1f)
        {
            moveState = MoveState.falling;
        }
        animator.SetInteger("moveState", (int)moveState);
    }

    private bool IsOnGround()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }
}
