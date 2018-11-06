using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    [SerializeField] BoolVariable pickedUp;
    [SerializeField] CharacterItemsList playerInventory;

    public Item pickUpItem;

    private void Start()
    {
        pickedUp.boolState = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Checkingg Trigger" + collision.name);
        pickedUp.boolState = collision.CompareTag("PickUp");

        if (pickedUp.boolState == true)
        {
            pickUpItem = collision.GetComponent<PickUpItem>().item;

            playerInventory.PickUpItemHandler(pickUpItem, false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pickedUp.boolState = false;
    }
}
