﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour {

    [SerializeField] Collider2D deadzoneBounds;
    [SerializeField] FloatReference playerX;
    [SerializeField] FloatReference playerY;
    [SerializeField] float moveSpeedMultiplier;
    [SerializeField] FloatReference playerFallingSpeed;
    [SerializeField] BoolVariable playerIsGrounded;

    private float oldPlayerY;

    private void FixedUpdate()
    {
        FollowTarget(playerX, playerY);
    }

    private void FollowTarget(FloatReference targetX, FloatReference targetY)
    {
        if(targetX.Value <  transform.position.x - deadzoneBounds.bounds.extents.x || targetX.Value > transform.position.x + deadzoneBounds.bounds.extents.x)
        {
            Vector3 moveVelocity = new Vector3(targetX.Value - transform.position.x, 0, 0);
            transform.Translate(moveVelocity * moveSpeedMultiplier * Time.deltaTime);
        }

        if ( targetY.Value > transform.position.y + deadzoneBounds.bounds.extents.y)
        {
            Vector3 moveVelocity = new Vector3(0, targetY.Value - transform.position.y, 0);
            transform.Translate(moveVelocity * moveSpeedMultiplier * Time.deltaTime);
        }

        else if (targetY.Value < transform.position.y)
        {
            Vector3 moveVelocity = new Vector3(0, playerFallingSpeed.Value, 0);
            transform.Translate(moveVelocity * Time.deltaTime);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(deadzoneBounds.bounds.center, deadzoneBounds.bounds.size);
    }
}
