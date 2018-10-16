using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public Image icon;
    public Button removeButton;

    public BoolVariable itemRemoved;
    public Item itemToRemove;
    public Text itemStackAmount;

    Item item;

    internal bool slotIsFull = false;

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.itemIcon;
        icon.enabled = true;
        removeButton.interactable = true;
        if(item.stackable == true && item.itemStack > 1)
        {
            itemStackAmount.text = item.itemStack.ToString();
            itemStackAmount.enabled = true;
        }
        else
        {
            itemStackAmount.enabled = false;
        }
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
        itemStackAmount.enabled = false;
    }

    public void OnItemRemove()
    {
        itemRemoved.boolState = true;
        //SET OTHER VALUES HERE
        itemToRemove.itemNumber = item.itemNumber;
        Debug.Log(itemToRemove.itemName);
    }
   
}
