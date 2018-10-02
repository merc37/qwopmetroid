using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : PlayerInput {

    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private bool facingRight = true;

    internal bool isGrounded;

    protected Rigidbody2D rb2d;


    void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
	
	protected override void FixedUpdate ()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = 0;

        base.FixedUpdate();

        rb2d.velocity = new Vector2(moveInput * speed, rb2d.velocity.y);

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }

    }

    private void Update()
    {

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;

        scaler.x *= -1;
        transform.localScale = scaler;
    }

}
