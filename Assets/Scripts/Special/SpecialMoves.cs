using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialMoves : MonoBehaviour {

    public BoolVariable playerDash;
    public BoolVariable playerDoubleJump;
    public BoolVariable playerWallClimb;

    public FloatReference extraJumps;

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
        if (playerDash.boolState == true && Input.GetButton("Dash"))
        {
            dashAbility.enabled = true;
        }
        else
        {
            dashAbility.enabled = false;
        }
        if (playerDoubleJump.boolState == true)
        {
            extraJumps.Variable.Value = 1;
        }
        else
        {
            extraJumps.Variable.Value = 0;
        }
        if(playerWallClimb.boolState == true)
        {
            Debug.Log("Can wall Climb");
        }
        else
        {
            //Debug.Log("Cannot wall Climb");
        }
        /*if (canDoubleJump)
          { 
              
          }*/
    }

}
