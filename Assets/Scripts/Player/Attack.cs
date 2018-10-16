using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public WeaponType weaponType;

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


	void Start () {
        startTimeBtwAttack = weaponType.timeBtwAttack.Value;
        weaponDmg = weaponType.Damage.Value;
        colType = weaponType.ColliderType;
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
            Collider2D[] enemyColliders = Physics2D.OverlapBoxAll(attackPos.transform.position, new Vector2(attackRangeX, attackRangeY) * 2, boxAngle, whatIsEnemy);
            for (int i = 0; i < enemyColliders.Length; i++)
            {
                enemyColliders[i].GetComponent<Enemy>().DamageTaken(weaponDmg);
                Debug.Log("EnemyMessageSent");
            }
        }else if (colliderTypeNumber == 1)
        {
            Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(attackPos.transform.position, attackRadius, whatIsEnemy);
            for (int i = 0; i < enemyColliders.Length; i++)
            {
                enemyColliders[i].GetComponent<Enemy>().DamageTaken(weaponDmg);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.transform.position, attackRadius);
        Gizmos.DrawWireCube(attackPos.transform.position, new Vector3(attackRangeX,attackRangeY,0));
    }
}
