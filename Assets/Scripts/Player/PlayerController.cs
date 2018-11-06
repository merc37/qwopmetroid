using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    [Header(header:"Down Slash Bounce")]
    public float downSlashJumpDistance;
    public float downSlashJumpTimeToFall;
    public float downSlashUpVelocity;
    [Space]
    [Header(header: "Character Related")]
    public FloatReference playerHealth;
    public BoolVariable playerWasDamaged;
    public FloatReference speed;
    [Space]
    [Header(header: "Player MoveInput Related")]
    public FloatReference moveInputX;
    public BoolVariable moveInputedX;
    public FloatReference moveInputY;
    public BoolVariable moveInputedY;
    public BoolVariable canMove;
    [Space]
    [Header(header: "Player Attack Related")]
    public Weapon currentWeapon;
    public BoolVariable playerAttacked;
    public BoolVariable damagedEnemy;
    [Space]
    [Header(header: "Player Inventory Related")]
    public EquippedItems playerEquippedItemsList;
    public CharacterItemsList playerInventoryList;
    [Space]
    [Header(header: "Player Physics Related")]
    public BoolVariable isGrounded;
    public float checkRadius;
    public LayerMask whatIsGround;
    public Transform groundCheck;

    [Space]
    [Header(header: "Temporary HERE")]
    public float knockBackX;
    public float knockBackY;
    public float knockBackTime;
    private bool playerKnockedBack;
    private float knockbackCoolDown;

    protected Rigidbody2D rb2d;

    private bool facingRight = true;

    private void OnValidate()
    {
        if (downSlashJumpTimeToFall != 0)
        {
            downSlashUpVelocity = (2 * downSlashJumpDistance) / downSlashJumpTimeToFall;
        }
    }

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
        if (canMove.boolState == true)
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
        else
        {
            if(playerKnockedBack == true)
            {
                if (Time.time >= knockbackCoolDown)
                {
                    canMove.boolState = true;
                    playerKnockedBack = false;
                }
            }
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
    
    private void BounceBack()
    {
        if (damagedEnemy.boolState == true && moveInputY.Value == -1)
        {
            Debug.Log("In HEre in BOUNCE BACK");
            rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Abs(downSlashUpVelocity));
            damagedEnemy.boolState = false;
        }
    }

    private void KnockBack(Vector2 positionOfContact, float knockBackSpeedY, float knockBackSpeedX)
    {
        if(transform.position.x > positionOfContact.x)
        {
            if(transform.position.y  > positionOfContact.y)
            {
                Vector2 directioin = new Vector2(1, 1).normalized;
                Vector2 directionForce = new Vector2(directioin.x * knockBackSpeedX, directioin.y * knockBackSpeedY);
                rb2d.velocity = directionForce;
                //rb2d.AddForce(directionForce);
            }
            else
            {
                Vector2 directioin = new Vector2(1, -1).normalized;
                Vector2 directionForce = new Vector2(directioin.x * knockBackSpeedX, directioin.y * knockBackSpeedY);
                rb2d.velocity = directionForce;
                //rb2d.AddForce(directionForce);
            }
        }
        else
        {
            if (transform.position.y > positionOfContact.y)
            {
                Vector2 directioin = new Vector2(-1, 1).normalized;
                Vector2 directionForce = new Vector2(directioin.x * knockBackSpeedX, directioin.y * knockBackSpeedY);
                rb2d.velocity = directionForce;
                //rb2d.AddForce(directionForce);
            }
            else
            {
                Vector2 directioin = new Vector2(-1, -1).normalized;
                Vector2 directionForce = new Vector2(directioin.x * knockBackSpeedX, directioin.y * knockBackSpeedY);
                rb2d.velocity = directionForce;
                //rb2d.AddForce(directionForce);
            }
        }

        playerKnockedBack = true;
        Debug.Log(rb2d.velocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            playerHealth.Variable.Value -= collision.gameObject.GetComponent<Enemy>().damagePlayer;
            canMove.boolState = false;
            knockbackCoolDown = Time.time + knockBackTime;
            KnockBack(collision.collider.transform.position, knockBackY, knockBackX);
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
