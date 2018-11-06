using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyWaypointMovement : MonoBehaviour {

    public FloatReference speed;
    public Collider2D[] waypoints;

    private int currentWaypointIndex;

    private Rigidbody2D rb2d;
    private bool facingRight = true;

    void Awake () {
        rb2d = GetComponent<Rigidbody2D>();
        currentWaypointIndex = 0;
	}
	
	void FixedUpdate () {
        Move(Mathf.Sign(waypoints[currentWaypointIndex].bounds.center.x - rb2d.position.x), speed);
	}

    protected void Move(float movementInput, FloatReference speedToMove) {

        rb2d.velocity = new Vector2(movementInput * speedToMove.Value, rb2d.velocity.y);

        if(facingRight == false && movementInput > 0) {
            Flip();
        } else if(facingRight == true && movementInput < 0) {
            Flip();
        }
    }

    private void Flip() {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;

        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log(collider.ToString());
        if(collider.bounds.center.Equals(waypoints[currentWaypointIndex].bounds.center)) {
            currentWaypointIndex++;
            if(currentWaypointIndex == waypoints.Length) {
                currentWaypointIndex = 0;
            }
        }
    }
}
