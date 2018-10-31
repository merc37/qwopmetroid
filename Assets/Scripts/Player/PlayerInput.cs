using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{ 
    public FloatReference HorizontalInputValue;
    public FloatReference VerticalInputValue;

    public BoolVariable HorizontalInputState;
    public BoolVariable VerticalInputState;

    public BoolVariable InventoryInputState;
    public BoolVariable openCloseShop;

    public BoolVariable canMoveInput;

    private void Start()
    {
        HorizontalInputState.boolState = false;
        InventoryInputState.boolState = false;
        canMoveInput.boolState = true;
    }

    private void FixedUpdate()
    {
        if(canMoveInput.boolState == true)
        {
            GetMovementInput();
        }

        GetUIOpenInput();
    }

    private void GetMovementInput()
    {
        //HORIZONTAL INPUT
        HorizontalInputValue.Variable.Value = Input.GetAxisRaw("Horizontal");
        if (Input.GetButton("Horizontal") || Input.GetButtonDown("Horizontal"))
        {
            HorizontalInputState.boolState = true;
        }
        else if (Input.GetButtonUp("Horizontal"))
        {
            HorizontalInputState.boolState = false;
        }
        else
        {
            HorizontalInputState.boolState = false;
        }

        //VERTICAL INPUT
        VerticalInputValue.Variable.Value = Input.GetAxisRaw("Vertical");
        if (Input.GetButton("Vertical") || Input.GetButtonDown("Vertical"))
        {
            VerticalInputState.boolState = true;
        }
        else if (Input.GetButtonUp("Vertical"))
        {
            VerticalInputState.boolState = false;
        }
        else
        {
            VerticalInputState.boolState = false;
        }

    }

    private void GetUIOpenInput()
    {
        //INVENTORY OPEN INPUT
        if (Input.GetButtonDown("Inventory"))
        {
            InventoryInputState.boolState = true;
            canMoveInput.boolState = !canMoveInput.boolState;
        }
        else
        {
            InventoryInputState.boolState = false;
        }

        if (Input.GetButtonDown("OpenShop"))
        {
            openCloseShop.boolState = true;
            canMoveInput.boolState = !canMoveInput.boolState;
        }
        else
        {
            openCloseShop.boolState = false;
        }

    }

}


