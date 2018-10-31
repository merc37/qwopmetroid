using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public WeaponType weaponType;
    public BoolVariable damagedEnemy;

    private float weaponDmg;
    private float timeBetweenAttack;
    private float startTimeBtwAttack;
    private float colType;

    private float boxAngle;

    private float attackRangeX;
    private float attackRangeY;
    private float attackRadius;

    public Transform attackPos;

    public LayerMask whatIsEnemy;

    public FloatReference[] playerInputs = new FloatReference[2];
    public BoolVariable[] playerInputBools = new BoolVariable[2];


    void Start () {
        startTimeBtwAttack = weaponType.timeBtwAttack.Value;
        weaponDmg = weaponType.Damage.Value;
        colType = weaponType.ColliderType;
        //Check collider on weapons type
        if (colType == 0)
        {
            attackRangeX = weaponType.AttackRangeX.Value;
            attackRangeY = weaponType.AttackRangeY.Value;
            attackRadius = 0;
        }
        else if(colType == 1)
        {
            attackRadius = weaponType.AttackRadius.Value;
            attackRangeX = 0;
            attackRangeY = 0;

        }
        boxAngle = weaponType.angleOfBox;
    }
	
	void Update () {
        if (timeBetweenAttack <= 0)
        {
            if (Input.GetButton("Attack"))
            {
                //Debug.Log("Pressed Attack");
                //Debug.Log(attackRangeX + ", " + attackRangeY);
                AttackTypeExecute(colType);
            }
            timeBetweenAttack = startTimeBtwAttack;
        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;
        }
	}

    private void AttackTypeExecute(float colliderTypeNumber)
    {
        if(colliderTypeNumber == 0)
        {
            Debug.Log("ColliderReached");

            Collider2D[] enemyColliders = Physics2D.OverlapBoxAll(AttackPosition(playerInputs, playerInputBools), new Vector2(attackRangeX, attackRangeY) * 2, boxAngle, whatIsEnemy);

            foreach (Collider2D col2d in enemyColliders)
            {
                col2d.GetComponent<Enemy>().DamageTaken(weaponDmg);
                damagedEnemy.boolState = true;
            }

        }

        else if (colliderTypeNumber == 1)
        {
            Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(AttackPosition(playerInputs, playerInputBools), attackRadius, whatIsEnemy);

            foreach (Collider2D col2D in enemyColliders)
            {
                col2D.GetComponent<Enemy>().DamageTaken(weaponDmg);
                damagedEnemy.boolState = true;
            }
        }
    }

    private Vector3 AttackPosition(FloatReference[] inputsPlayer, BoolVariable[] playerInputBools)
    {
        Vector3 attackPosition = new Vector3(transform.position.x, transform.position.y, 0);

        if (playerInputBools[1].boolState == true)
        {
            attackPosition.x = transform.position.x;
            attackPosition.y = transform.position.y + inputsPlayer[1].Value;
            attackPosition.z = 0;
        }
        if (playerInputBools[0].boolState == true)
        {
            attackPosition.x = transform.position.x + inputsPlayer[0].Value;
            attackPosition.y = transform.position.y;
            attackPosition.z = 0;
        }
        else if(playerInputBools[0].boolState ==  false && playerInputBools[1].boolState == false)
        {
            attackPosition.x = transform.position.x;
            attackPosition.y = transform.position.y;
            attackPosition.z = 0;
        }

        return attackPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPosition(playerInputs,playerInputBools), attackRadius);
        Gizmos.DrawWireCube(AttackPosition(playerInputs, playerInputBools), new Vector3(attackRangeX,attackRangeY,0));
    }
}
