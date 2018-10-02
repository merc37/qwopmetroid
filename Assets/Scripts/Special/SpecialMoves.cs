using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialMoves : MonoBehaviour {

    public Dash dashAbility;
    public DoubleJump doubleJumpAbility;
    public PlayerController playerController;

    void Start () {
        dashAbility = GetComponent<Dash>();
        playerController = GetComponent<PlayerController>();
        doubleJumpAbility = GetComponent<DoubleJump>();

        dashAbility.enabled = false;
        playerController.enabled = true;
	}
	
	void Update () {
        CheckSpecialActivate();
        CheckSpecialActivate();
	}

    void CheckSpecialActivate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            dashAbility.enabled = true;
            playerController.enabled = false;
        }
        else
        {
            dashAbility.enabled = false;
            playerController.enabled = true;
        }
    }

}
