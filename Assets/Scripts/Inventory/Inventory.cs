using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public ShopList playerInventoryList;
    public ShopList displayPlayerInventoryList;

    public Item pickupItem;
    public Item itemToRemove;

    public float inventorySpace;

    public BoolVariable pickedItem;
    public BoolVariable isItemToRemove;

    public BoolVariable inventoryKey;
    [SerializeField] GameObject inventoryUI;

    public FloatReference numberOfItemsWithPlayer;

    private void Start()
    {
        pickedItem.boolState = false;
        numberOfItemsWithPlayer.Variable.Value = playerInventoryList.itemsList.Count;
    }

    private void Update()
    {
        if (inventoryKey.boolState == true)
        {
            ActivateInventory();
            inventoryKey.boolState = false;
        }

    }

    private void LateUpdate()
    {
        if (pickedItem.boolState == true && pickupItem != null)
        {
            PickedUpItem(pickupItem);
            pickedItem.boolState = false;
        }

        if (isItemToRemove.boolState == true)
        {
            RemoveItem(itemToRemove);
            Debug.Log(itemToRemove.itemName + "from removeItem");
            isItemToRemove.boolState = false;
        }
    }

    private void PickedUpItem(Item item)
    {
        if(playerInventoryList.itemsList.Count < inventorySpace)
        {
            AddItem(item);
            //Debug.Log(item.itemName + " added to inventory");
        }
        else
        {
            Debug.Log("Inventory is Full");
            Debug.Log(playerInventoryList.itemsList.Count);
        }
    }

    private void AddItem(Item item)
    {
        if (item != null)
        {
            int itemIndex = playerInventoryList.itemsList.IndexOf(item);

            if (playerInventoryList.itemsList.Contains(item) && playerInventoryList.itemsList[itemIndex].stackable == true)
            {
                playerInventoryList.itemsList[itemIndex].itemStack++;
                displayPlayerInventoryList.itemsList = playerInventoryList.itemsList;
            }
            else
            {
                playerInventoryList.itemsList.Add(item);
                displayPlayerInventoryList.itemsList = playerInventoryList.itemsList;
            }
        }
        else
        {
            Debug.LogWarning("THIS IS A WARNING item IS NULLcant add item");
        }
    }

    private void RemoveItem(Item item)
    {
        if (item != null)
        {
            int itemIndex = item.itemNumber;

            for (int i = 0; i < playerInventoryList.itemsList.Count; i++)
            {
                if( itemIndex == playerInventoryList.itemsList[i].itemNumber)
                {
                    if (playerInventoryList.itemsList[i].stackable == true)
                    {
                        playerInventoryList.itemsList[itemIndex].itemStack--;
                        displayPlayerInventoryList.itemsList = playerInventoryList.itemsList;
                        Debug.Log("stackableItemHasBeenRemoved");
                    }
                    else
                    {
                        playerInventoryList.itemsList.Remove(item);
                        displayPlayerInventoryList.itemsList = playerInventoryList.itemsList;
                        Debug.Log("ItemHasBeenRemoved");
                    }
                }
            }
            
        }
        else
        {
            Debug.LogWarning("THIS IS A WARNING item IS NULL cant remove item");
        }
    }

    private void ActivateInventory()
    {
        if (inventoryKey)
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            Debug.Log(!inventoryUI.activeSelf);
        }
    }
}
