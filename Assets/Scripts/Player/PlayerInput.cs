using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{ 
    [Header(header: "Move Inputs & Bools")]
    public FloatReference HorizontalInputValue;
    public FloatReference VerticalInputValue;

    public BoolVariable HorizontalInputState;
    public BoolVariable VerticalInputState;
    public BoolVariable jumpInputState;
    [Space]
    [Header(header: "OtherInputs")]
    public BoolVariable AttackInputState;

    public BoolVariable InventoryInputState;
    public BoolVariable openCloseShop;

    public BoolVariable canMoveInputX;
    public BoolVariable canMoveInputY;

    public FloatReference totalMovesLeft;
    public FloatReference totalMovesRight;
    public FloatReference totalMovesDown;
    public FloatReference totalMovesUp;

    [SerializeField] BoolVariable restrictedLeft;
    [SerializeField] BoolVariable restrictedRight;
    [SerializeField] BoolVariable restrictedDown;
    [SerializeField] BoolVariable restrictedUp;

    private float oldAixsValuesX;
    private float oldAixsValuesY;

    private void Start()
    {
        //restrictedRight = true;
        HorizontalInputState.boolState = false;
        InventoryInputState.boolState = false;
        canMoveInputX.boolState = true;
        canMoveInputY.boolState = true;
        jumpInputState.boolState = false;
    }

    private void FixedUpdate()
    {
        //if(canMoveInputX.boolState == true)
        //{
        //    GetMovementInput();
        //}
        GetMovementInput();
        GetUIOpenInput();
    }

    //MOVEMENT INPUTS FOR NOW CONTAINS ATTTACK INPUTS
    private void GetMovementInput()
    {
        #region HORIZONTAL INPUT

        HorizontalInputValue.Variable.Value = Input.GetAxisRaw("Horizontal");

        if (Input.GetButton("Horizontal") || Input.GetButtonDown("Horizontal"))
        {
            HorizontalInputState.boolState = true;
            PlayerMovementRestrictor(restrictedLeft.boolState, restrictedRight.boolState, HorizontalInputValue, HorizontalInputState, canMoveInputX);
            oldAixsValuesX = HorizontalInputValue.Value;
        }
        else if (Input.GetButtonUp("Horizontal"))
        {
            //Debug.Log(oldAixsValuesX);
            TotalMovesHandler(restrictedLeft.boolState, restrictedRight.boolState, oldAixsValuesX, totalMovesLeft, totalMovesRight);
            RestrictionHandelr(restrictedLeft, restrictedRight, oldAixsValuesX, totalMovesLeft, totalMovesRight);
            HorizontalInputState.boolState = false;
            oldAixsValuesX = 0;
        }
        else
        {
            HorizontalInputState.boolState = false;
            oldAixsValuesX = 0;
        }

        #endregion

        #region VERTICAL INPUT

        VerticalInputValue.Variable.Value = Input.GetAxisRaw("Vertical");

        if (Input.GetButton("Vertical") || Input.GetButtonDown("Vertical"))
        {
            VerticalInputState.boolState = true;
            PlayerMovementRestrictor(restrictedDown.boolState, restrictedUp.boolState, VerticalInputValue, VerticalInputState, canMoveInputY);
            oldAixsValuesY = VerticalInputValue.Value;
        }
        else if (Input.GetButtonUp("Vertical"))
        {
            TotalMovesHandler(restrictedDown.boolState, restrictedUp.boolState, oldAixsValuesY, totalMovesDown, totalMovesUp);
            RestrictionHandelr(restrictedDown, restrictedUp, oldAixsValuesY, totalMovesDown, totalMovesUp);
            VerticalInputState.boolState = false;
            oldAixsValuesY = 0;
        }
        else
        {
            VerticalInputState.boolState = false;
            oldAixsValuesY = 0;
        }
        #endregion
        
        #region Attack Input
        if (Input.GetButton("Attack") || Input.GetButtonDown("Attack"))
        {
            AttackInputState.boolState = true;
        }
        else if (Input.GetButtonUp("Attack"))
        {
            AttackInputState.boolState = false;
        }
        else
        {
            AttackInputState.boolState = false;
        }
        #endregion
    }

    //OPENING AND CLOSING UI
    private void GetUIOpenInput()
    {
        //INVENTORY OPEN INPUT
        if (Input.GetButtonDown("Inventory"))
        {
            InventoryInputState.boolState = true;
            canMoveInputX.boolState = !canMoveInputX.boolState;
        }
        else
        {
            InventoryInputState.boolState = false;
        }

        if (Input.GetButtonDown("OpenShop"))
        {
            openCloseShop.boolState = true;
            canMoveInputX.boolState = !canMoveInputX.boolState;
        }
        else
        {
            openCloseShop.boolState = false;
        }

    }

    private void TotalMovesHandler(bool _restrictLessDir, bool _restrictGreaterDir, float directionAxis, FloatReference totalMovesLessDir, FloatReference totalMovesGreaterDir)
    {
        if (directionAxis < 0)
        {
            if (_restrictLessDir == false)
            {
                totalMovesLessDir.Variable.Value -= 1;
            }
        }
        else if (directionAxis > 0)
        {
            if (_restrictGreaterDir == false)
            {
                totalMovesGreaterDir.Variable.Value -= 1;
            }
        }
    }

    private void PlayerMovementRestrictor(bool _restrictLessDir, bool _restrictGreaterDir, FloatReference directionAxis, BoolVariable moveAxisBool, BoolVariable canMoveInput)
    {
        if(directionAxis.Value < 0)
        {
            if(_restrictLessDir == false)
            {
                canMoveInput.boolState = true;
                moveAxisBool.boolState = true;
            }
            else
            {
                canMoveInput.boolState = false;
                moveAxisBool.boolState = false;
                directionAxis.Variable.Value = 0;
            }
        }
        else if(directionAxis.Value > 0)
        {
            if (_restrictGreaterDir == false)
            {
                canMoveInput.boolState = true;
                moveAxisBool.boolState = true;
            }
            else
            {
                Debug.Log("Stopped From Moving");
                canMoveInput.boolState = false;
                moveAxisBool.boolState = false;
                directionAxis.Variable.Value = 0;
            }
        }
    }
    private void PlayerMovementJumpRestrictor(bool _restrictLessDir, bool _restrictGreaterDir, FloatReference directionAxis, BoolVariable moveAxisBool, BoolVariable canMoveInput, BoolVariable jumpBool)
    {
        if(directionAxis.Value < 0)
        {
            if(_restrictLessDir == false)
            {
                canMoveInput.boolState = true;
                moveAxisBool.boolState = true;
            }
            else
            {
                canMoveInput.boolState = false;
                moveAxisBool.boolState = false;
                directionAxis.Variable.Value = 0;
            }
        }
        else if(directionAxis.Value > 0)
        {
            if (_restrictGreaterDir == false)
            {
                jumpBool.boolState = true;
                canMoveInput.boolState = true;
                moveAxisBool.boolState = true;
            }
            else
            {
                Debug.Log("Stopped From Moving");
                jumpBool.boolState = false;
                canMoveInput.boolState = false;
                moveAxisBool.boolState = false;
                directionAxis.Variable.Value = 0;
            }
        }
    }

    private void RestrictionHandelr(BoolVariable _restrictLessDir, BoolVariable _restrictGreaterDir, float directionAxis, FloatReference totalMovesLessDir, FloatReference totalMovesGreaterDir)
    {
        if(directionAxis < 0)
        {
            if(totalMovesLessDir.Value < 1)
            {
                _restrictLessDir.boolState = true;
                //Debug.Log("set restrictLessDir" + _restrictLessDir);
            }
            else
            {
                _restrictLessDir.boolState = false;
                //Debug.Log("set restrictLessDir " + _restrictLessDir);
            }
        }
        else if(directionAxis > 0)
        {
            if (totalMovesGreaterDir.Value >= 1)
            {
                _restrictGreaterDir.boolState = false;
                //Debug.Log("set restrictGreaterDir " + _restrictGreaterDir);
            }
            else
            {
                _restrictGreaterDir.boolState = true;
                //Debug.Log("set restrictGreaterDir " + _restrictGreaterDir);
            }
        }
    }

}


