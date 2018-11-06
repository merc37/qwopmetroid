using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float health;
    public float damagePlayer = 5;

    private float initialEnemyHealth;

	void Start () {
        initialEnemyHealth = health;
	}
	
	void Update () {
        if (health <= 0)
        {
            health = initialEnemyHealth;
            Destroy(gameObject);
        }
	}
    public void DamageTaken(float dmgFromPlayer)
    {
        health -= dmgFromPlayer;
        Debug.Log("DamageTaken");
    }
}
