using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialMoves : MonoBehaviour {

    public GameObject player;

    private Dash dashAbility;
    private DoubleJump doubleJumpAbility;
    
    //private bool canDoubleJump = false;

    private void Awake()
    {
        dashAbility = player.GetComponent<Dash>();
        doubleJumpAbility = player.GetComponent<DoubleJump>();

        dashAbility.enabled = false;
        doubleJumpAbility.enabled = true;
 
    }

    void Start () {
	}
	
	void Update () {
        ChecktToActivate();
	}

    protected void ChecktToActivate()
    {
        if (Input.GetButton("Dash"))
        {
            dashAbility.enabled = true;
        }
        else
        {
            dashAbility.enabled = false;
        }

        /*if (canDoubleJump)
          { 
              
          }*/
    }

}
