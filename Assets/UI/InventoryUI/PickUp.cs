﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    [SerializeField] BoolVariable pickedUp;
    [SerializeField] ShopList playerInventory;

    private Item item;

    private void Start()
    {
        pickedUp.boolState = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        pickedUp.boolState = collision.collider.CompareTag("PickUp");

        if (pickedUp.boolState == true)
        {
            item = collision.collider.GetComponent<PickUpItem>().item;
            playerInventory.itemsList.Add(item);
            playerInventory.AddItemToStack(item);
            //Debug.Log(item.itemName + "Set to item");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        pickedUp.boolState = false;
        item = null;
    }
}
