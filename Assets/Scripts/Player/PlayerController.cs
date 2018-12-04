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
    //public BoolVariable playerWasDamaged;
    public FloatReference speed;
    [Space]
    [Header(header: "Player MoveInput Related:")]
    public FloatReference moveInputX;
    public BoolVariable moveInputedX;
    public FloatReference moveInputY;
    public float oldMoveInputY;
    public BoolVariable moveInputedY;
    public BoolVariable canMoveX;
    public BoolVariable canMoveY;
    [Space]
    [Header(header: "Player Jump Related:")]
    public int remainingJumps;
    public FloatReference extraJumps;
    public float minJumpHeight;
    public float maxJumpHeight;
    public float halfJumpTime;
    public float maxJumpSpeed;
    public float minJumpSpeed;
    public BoolVariable canJump;
    public BoolVariable jumpRestricted;
    private bool isJumping;

    public float fallMultiplyer = 2.5f;
    public float lowMultiplyer = 2.0f;
    [Space]
    [Header(header: "Player Attack Related:")]
    public Weapon currentWeapon;
    public BoolVariable playerAttacked;
    public BoolVariable damagedEnemy;
    [Space]
    [Header(header: "Player Inventory Related:")]
    public EquippedItems playerEquippedItemsList;
    public CharacterItemsList playerInventoryList;
    public BoolVariable canOpenMemoryPannel;
    [Space]
    [Header(header: "Player Physics Related:")]
    public BoolVariable isGrounded;
    public float checkRadius;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float checkForCeelingAtHeight;
    public LayerMask whatIsCelling;
    [Space]
    [Header(header: "Player Wall Climb:")]
    public BoolVariable playerCanClimb;
    public bool wallClimbing;
    public LayerMask whatIsWall;
    public float wallSlideSpeed;
    public float wallLeapXSpeed;
    public float wallJumpYSpeed;
    [Space]
    [Header(header: "Temporary Here:")]
    public float knockBackX;
    public float knockBackY;
    public float knockBackTime;
    private bool playerKnockedBack;
    private float knockbackCoolDown;

    [SerializeField] FloatReference playerX;
    [SerializeField] FloatReference playerY;
    [SerializeField] FloatReference playerFallingSpeed;

    protected Rigidbody2D rb2d;

    public BoolVariable facingRight;

    private BoxCollider2D playerBoxCollider2d;

    private void OnValidate()
    {
        if (downSlashJumpTimeToFall != 0)
        {
            downSlashUpVelocity = (2 * downSlashJumpDistance) / downSlashJumpTimeToFall;
        }
        if(halfJumpTime != 0)
        {
            maxJumpSpeed = (2 * maxJumpHeight) / halfJumpTime;
        }
        if(minJumpHeight != 0)
        {
            minJumpSpeed = (2 * minJumpHeight) / halfJumpTime;
        }

        playerHealth.Variable.Value = 10;
    }

    private void Awake ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerBoxCollider2d = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        ResetJump(true);
        facingRight.boolState = true;
    }

    private void Update()
    {
        BounceBack();
        playerX.Variable.Value = transform.position.x;
        playerY.Variable.Value = transform.position.y;
        playerFallingSpeed.Variable.Value = rb2d.velocity.y;
    }

    private void FixedUpdate ()
    {
        if (canMoveX.boolState == true)
        {
            MovementX(moveInputX.Value, speed, isGrounded);
        }
        else
        {
            if(playerKnockedBack == true)
            {
                if (Time.time >= knockbackCoolDown)
                {
                    canMoveX.boolState = true;
                    playerKnockedBack = false;
                }
            }
        }
        if(canMoveY.boolState == true)
        {
            MovementY(moveInputY.Value, speed, canJump, isGrounded);
        }
        WallClimb(playerCanClimb);
    }

    protected void MovementX(float movementInputX, FloatReference speedToMoveX,  BoolVariable isGrounded)
    {
        isGrounded.boolState = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsGround);
        if(isGrounded.boolState == true)
        {
            wallClimbing = false;
        }
        rb2d.velocity = new Vector2(movementInputX * speedToMoveX.Value, rb2d.velocity.y);

        if (facingRight.boolState == false && movementInputX > 0)
        {
            Flip();
        }
        else if (facingRight.boolState == true && movementInputX < 0)
        {
            Flip();
        }
    }
    
    protected void MovementY(float movementInputY, FloatReference speedToMoveY, BoolVariable canJump, BoolVariable isGrounded)
    {
        if (canJump.boolState == true)
        {
            FastFall();
            rb2d.gravityScale = 1;
            if(wallClimbing  == false)
            {
                Jump();
            }
        }
        if (Physics2D.OverlapCircle((Vector2)transform.position + Vector2.up * checkForCeelingAtHeight, checkRadius, whatIsCelling) && rb2d.velocity.y >  0)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            ResetJump(false);
        }
    }

    void Jump()
    {
        if (isGrounded.boolState == true)
        {
            //Debug.Log("Reseting the jump");
            ResetJump(true);
        }
        if (((Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0) && remainingJumps > 0) && jumpRestricted.boolState == false)
        {
            //Debug.Log("Is Jumping");
            //Debug.Log("moveInputY was Greater than 0");
            //Debug.Log("Doing this from remainingExtrajumps");
            rb2d.velocity = new Vector2(rb2d.velocity.x, maxJumpSpeed);
            isJumping = true;
            remainingJumps--;
            oldMoveInputY = moveInputY.Value;
            //Debug.Log(remainingJumps);
        }

        if (Input.GetButtonUp("Vertical") && Input.GetAxisRaw("Vertical") >= 0)
        {
            //Debug.Log("oldMoveInputY was Greater than 0");
            //Debug.Log("Doing this from jump button up");
            if (rb2d.velocity.y > minJumpSpeed)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, minJumpSpeed);
            }
            isJumping = false;
            oldMoveInputY = 0;
        }
    }

    private void WallClimb(BoolVariable playerCanClimb)
    {
        if(playerCanClimb.boolState == true)
        {
            Vector3 checkPosition = new Vector3(transform.position.x + (moveInputX.Variable.Value * 0.1f), transform.position.y, transform.position.z);
            if (Physics2D.OverlapBox(checkPosition, playerBoxCollider2d.bounds.size, 0, whatIsWall))
            {
                if (isGrounded.boolState == false && rb2d.velocity.y < 0)
                {
                    if (Input.GetButtonDown("Vertical") && Input.GetButton("Horizontal") && jumpRestricted.boolState == false)
                    {
                        rb2d.velocity = new Vector2(wallLeapXSpeed * -moveInputX.Variable.Value, wallJumpYSpeed);
                        wallClimbing = true;
                    }
                    else if (Input.GetButton("Horizontal"))
                    {
                        rb2d.velocity = new Vector2(rb2d.velocity.x, wallSlideSpeed);
                        wallClimbing = true;
                    }
                    else
                    {
                        wallClimbing = false;
                    }
                }
                else
                {
                    wallClimbing = false;
                }
            }
        }
        else
        {
            //Debug.Log("Cannot wall Climb");
        }
    }

    private void FastFall()
    {
        if(rb2d.velocity.y > 0)
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplyer - 1) * Time.deltaTime;
        }
        if (rb2d.velocity.y < 0)
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplyer - 1) * Time.deltaTime;
        }
        else if (Input.GetButtonUp("Jump") && rb2d.velocity.y > 0)
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowMultiplyer - 1) * Time.deltaTime;
        }
    }

    private void BounceBack()
    {
        if (damagedEnemy.boolState == true && moveInputY.Value == -1)
        {
            //Debug.Log("In HEre in BOUNCE BACK");
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
        //Debug.Log(rb2d.velocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            //playerHealth.Variable.Value -= collision.gameObject.GetComponent<EnemyProperties>().damageAmount;
            canMoveX.boolState = false;
            knockbackCoolDown = Time.time + knockBackTime;
            KnockBack(collision.collider.transform.position, knockBackY, knockBackX);
            //Debug.Log(playerHealth.Variable.Value);
        }
        else if (collision.collider.CompareTag("Chest"))
        {
            canOpenMemoryPannel.boolState = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Chest"))
        {
            canOpenMemoryPannel.boolState = false;
        }
    }

    private void Flip()
    {
        facingRight.boolState = !facingRight.boolState;
        Vector3 scaler = transform.localScale;

        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void ResetJump(bool fullReset)
    {
        if (fullReset)
        {
            remainingJumps = (int)extraJumps.Variable.Value + 1;
            isJumping = false;
        }
        else
        {
            isJumping = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawWireCube(transform.position, transform.lossyScale * 2);
        //Gizmos.DrawWireSphere((Vector2)transform.position + Vector2.up * checkForCeelingAtHeight, checkRadius);
        Gizmos.DrawCube(new Vector3(transform.position.x + moveInputX.Variable.Value, transform.position.y, transform.position.z),new Vector2(1,1));

    }
}
