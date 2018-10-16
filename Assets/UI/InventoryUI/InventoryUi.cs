using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUi : MonoBehaviour {

    public ShopList displayPlayerInventoryList;
    public ShopList playerInventoryList;
    public FloatReference numberOfItemsWithPlayer;

    [SerializeField] BoolVariable[] interactedWithItem = new BoolVariable[3];

    [SerializeField] Transform itemsParent;
    InventorySlot[] slots;

    private float itemProduct;
    private bool stackItemExists;

    private void Start()
    {
        slots = GetComponentsInChildren<InventorySlot>();
        UpdateUI();
    }

    private void Update()
    {
        for (int i = 0; i < interactedWithItem.Length; i++)
        {
            if (interactedWithItem[i].boolState == true)
            {

                UpdateUI();

                // DIFFERENT METHOD BUT NOT SO GREAT OUTCOME
                //if (numberOfItemsWithPlayer.Value < displayPlayerInventoryList.itemsList.Count)
                //{
                //    Debug.Log("entered numberofplayer");
                //    UpdateUI();
                //    numberOfItemsWithPlayer.Variable.Value++;
                //    //interactedWithItem[i].boolState = false;
                //    //Debug.Log("SetFalse to " + interactedWithItem[i].name.ToString());
                //}
            }
        }
       // Debug.Log(interactedWithItem[1]);
    }


    private void UpdateUI()
    {
        displayPlayerInventoryList.itemsList = playerInventoryList.itemsList;

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < displayPlayerInventoryList.itemsList.Count)
            {
                slots[i].AddItem(displayPlayerInventoryList.itemsList[i]);
                //Debug.Log(displayPlayerInventoryList.itemsList[i].itemName);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
        
}
