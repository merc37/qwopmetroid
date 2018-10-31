using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public Image icon;
    public Button removeButton;
    public ShopList playerInventory;

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
        if(item.stackable == true && playerInventory.ItemAndStackNumber(item).y > 1)
        {
            itemStackAmount.text = playerInventory.ItemAndStackNumber(item).y.ToString();
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
        if(item.stackable == true)
        {
            if (playerInventory.ItemAndStackNumber(item).y == 1)
            {
                playerInventory.itemsList.Remove(item);
                playerInventory.RemoveItemFromStack(item);
            }
            else
            {
                playerInventory.DecItemStackNumber(item);
            }
            
        }
        else
        {
            playerInventory.itemsList.Remove(item);
            playerInventory.RemoveItemFromStack(item);
        }
        
        ClearSlot();
    }
   
}
