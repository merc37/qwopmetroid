using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour {

    private int HorizontalDown;
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
        if (Input.GetButtonDown("Horizontal") && dashTime < Time.time)
        {
            float direction = Input.GetAxisRaw("Horizontal");
            dashTime = Time.time + dashCooldownTime;
            HorizontalDown++;
            Instantiate(dashEffect, rb.position, Quaternion.identity);
            DashPlayer(direction);
        }
    }

    void DashPlayer(float direction)
    {
        if (HorizontalDown == 1)
        {
            rb.velocity = new Vector2(direction, rb.velocity.y) * dashSpeed;
            shakeScript.shouldShake = true;
            Debug.Log("Dash LEFT");
        }
    }

    void ResetDash()
    {
        HorizontalDown = 0;
    }
}
