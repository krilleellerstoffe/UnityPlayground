using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float dirX;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private LayerMask ground;
    
    //enum for storing different movement states
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
        if (Input.GetButtonDown("Jump") && IsOnGround()) //only jump if on ground
        {
            body.velocity = new Vector2(body.velocity.x, jumpForce);
        }
        UpdateAnimationState();
        LookInMouseDirection();
    }
    //Orient player sprite in mouse's direction
    private void LookInMouseDirection()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePosition - transform.position;
        float angle = Vector2.SignedAngle(Vector2.up, direction);
        if (angle < 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    //Updates the sprite's animation based on movement
    private void UpdateAnimationState()
    {
        MoveState moveState;
       
        if(dirX > 0f) //if moving to right
        {
            //spriteRenderer.flipX = false;
            moveState = MoveState.running;
        }
        else if(dirX < 0f) //if moving to left
        {
            //spriteRenderer.flipX = true;
            moveState = MoveState.running;
        }
        else //if still
        {
            moveState = MoveState.idling;
        }
        
        if(body.velocity.y > 0.1f) //if jumping
        {
            moveState = MoveState.jumping;
        }
        else if (body.velocity.y < -0.1f) //if falling
        {
            moveState = MoveState.falling;
        }
        //set animation based on moveState
        animator.SetInteger("moveState", (int)moveState);
    }
    //Creates a boxcollider slightly lower than the player sprite's
    //returns true if it is touching the ground
    private bool IsOnGround()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, ground);
    }
}
