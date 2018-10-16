using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{ 
    public FloatReference HorizontalInputValue;
    public BoolVariable HorizontalInputState;
    public BoolVariable InventoryInputState;

    private void Start()
    {
        HorizontalInputState.boolState = false;
        InventoryInputState.boolState = false;
    }

    private void FixedUpdate()
    {
        HorizontalInputValue.Variable.Value = Input.GetAxisRaw("Horizontal");

        if (Input.GetButton("Horizontal") || Input.GetButtonDown("Horizontal"))
        {
            HorizontalInputState.boolState = true;
        }
        else if(Input.GetButtonUp("Horizontal"))
        {
            HorizontalInputState.boolState = false;
        }
        else
        {
            HorizontalInputState.boolState = false;
        }

        if (Input.GetButtonDown("Inventory"))
        {
            InventoryInputState.boolState = true;
        }
        else
        {
            InventoryInputState.boolState = false;
        }


    }

}
