using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour {

    private int leftDown;
    private int rightDown;

    public float dashSpeed;
    public float dashCooldownTime;
    public float dashTime;

    public GameObject dashEffect;
    public GameObject Camera;

    private Rigidbody2D rb;
    private CameraShake shakeScript;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        shakeScript = Camera.GetComponent<CameraShake>();
    }

    void FixedUpdate () {
        ResetDash();
        DashExecute();
	}

    void DashExecute()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && dashTime < Time.time)
        {
            dashTime = Time.time + dashCooldownTime;
            leftDown++;
            Instantiate(dashEffect, rb.position, Quaternion.identity);
            DashPlayer();
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow) && dashTime < Time.time)
        {
            dashTime = Time.time + dashCooldownTime;
            rightDown++;
            Instantiate(dashEffect, rb.position, Quaternion.identity);
            DashPlayer();
        }
    }

    void DashPlayer()
    {
        if (leftDown == 1)
        {
            rb.velocity = Vector2.left * dashSpeed;
            shakeScript.shouldShake = true;
            Debug.Log("Dash LEFT");
        }


        else if (rightDown == 1)
        {
            rb.velocity = Vector2.right * dashSpeed;
            shakeScript.shouldShake = true;
            Debug.Log("Dash Right");
        }
    }

    void ResetDash()
    {
        leftDown = 0;
        rightDown = 0;
        rb.velocity = Vector2.zero;
    }
}
