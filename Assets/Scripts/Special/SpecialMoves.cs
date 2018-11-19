using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialMoves : MonoBehaviour {

    public GameObject player;

    private Dash dashAbility;
    
    //private bool canDoubleJump = false;

    private void Awake()
    {
        dashAbility = player.GetComponent<Dash>();

        dashAbility.enabled = false;
 
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
