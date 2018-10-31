using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    public BoolVariable damagedEnemy; 

    public FloatReference speed;

    public FloatReference moveInputX;
    public BoolVariable moveInputedX;
    public FloatReference moveInputY;
    public BoolVariable moveInputedY;

    public BoolVariable isGrounded;
    public float checkRadius;
    public LayerMask whatIsGround;
    public Transform groundCheck;

    public FloatReference playerHealth;

    protected Rigidbody2D rb2d;

    private bool facingRight = true;

    void Awake ()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        BounceBack();
    }

    private void FixedUpdate ()
    {
        if (moveInputedX.boolState)
        {
            Move(moveInputX.Value, speed);
            //Debug.Log("moving: "+ moveInputX.Value);
        }
        else
        {
            Move(0, speed);
            //Debug.Log("stillX: " + moveInputX.Value);
        }
    }

    protected void Move(float movementInput, FloatReference speedToMove)
    {
        isGrounded.boolState = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        rb2d.velocity = new Vector2(movementInput * speedToMove.Value, rb2d.velocity.y);

        if (facingRight == false && movementInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && movementInput < 0)
        {
            Flip();
        }
    }
    
    //FIX CODE BELOW IT IS NOT WORKING
    private void BounceBack()
    {
        if (damagedEnemy.boolState == true && moveInputY.Value == -1)
        {
            rb2d.AddForce(Vector2.up * 10000);
            damagedEnemy.boolState = false;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;

        scaler.x *= -1;
        transform.localScale = scaler;
    }

}
