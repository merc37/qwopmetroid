using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    protected float moveInput;
    public float speed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	protected virtual void FixedUpdate () {

        moveInput = Input.GetAxisRaw("Horizontal");
 
    }

}
