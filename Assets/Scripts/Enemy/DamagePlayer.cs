using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour {

    public FloatReference playerHealth;
    [SerializeField] EnemyProperties propertiesOfThisEnemy;

    private void Start()
    {
        propertiesOfThisEnemy = GetComponent<EnemyProperties>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerHealth.Variable.Value -= propertiesOfThisEnemy.damageAmount;
        }
    }

}
