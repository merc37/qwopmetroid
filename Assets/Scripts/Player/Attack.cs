using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    private float timeBetweenAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemy;
    public float attackRange;
    public float damage;

	void Start () {
		
	}
	
	void Update () {
        if (timeBetweenAttack <= 0)
        {
            if (Input.GetButton("Attack"))
            {
                Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
                for (int i = 0; i < enemyColliders.Length; i++)
                {
                    enemyColliders[i].GetComponent<Enemy>().DamageTaken(damage);
                }
            }
            timeBetweenAttack = startTimeBtwAttack;
        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;
        }
	}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
