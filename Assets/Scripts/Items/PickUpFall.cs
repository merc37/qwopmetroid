﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpFall : MonoBehaviour {

    public LayerMask whatIsGround;
    public Vector2 fallSpeed;
    public bool isGrounded;

    private Vector2 oldFallSpeed;

    // Use this for initialization
    private void OnValidate () {
        oldFallSpeed = fallSpeed;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!isGrounded)
        {
            InstantiatedHorizontalMovement();
            FallToGround();
        }	
	}

    private void InstantiatedHorizontalMovement()
    {
        if(fallSpeed.x != 0)
        {
            transform.Translate(fallSpeed.x * Time.deltaTime, 0, 0);
        }
    }

    private void FallToGround()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, whatIsGround);
        if(hitInfo)
        {
            if (hitInfo.transform.CompareTag("Ground"))
            {
                if (hitInfo.distance > 1.0f)
                {
                    transform.Translate(fallSpeed * Time.deltaTime);
                    fallSpeed.y = -Mathf.Sqrt(Mathf.Pow(fallSpeed.y, 2) + 20 * hitInfo.distance);
                }
                else
                {
                    isGrounded = true;
                    fallSpeed = oldFallSpeed;
                }
            }
        }
    }

}
