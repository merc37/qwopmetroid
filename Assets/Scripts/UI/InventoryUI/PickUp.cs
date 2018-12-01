﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    [SerializeField] BoolVariable pickedUpItem;
    [SerializeField] BoolVariable abilityPickedUp;
    [SerializeField] CharacterItemsList playerInventory;
    [SerializeField] CharacterAbilitiesList playerAbilitiesList;

    private PickUpItem pickUpItem;
    private AbilityPickUp abilityPickUp;

    private void Start()
    {
        pickedUpItem.boolState = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Checkingg Trigger" + collision.name);
        pickedUpItem.boolState = collision.CompareTag("PickUp");

        if (pickedUpItem.boolState == true)
        {
            pickUpItem = collision.GetComponent<PickUpItem>();

            if(playerInventory.PickUpItemHandler(pickUpItem.item, false) && pickUpItem.destroyItem == false)
            {
                pickedUpItem.boolState = false;
                pickUpItem.destroyItem = true;
            }
            else
            {
                pickedUpItem.boolState = false;
                pickUpItem.destroyItem = false;
            }
        }
        else if (collision.CompareTag("Ability"))
        {
            abilityPickUp = collision.GetComponent<AbilityPickUp>();

            if (playerAbilitiesList.AddAndActivateAbility(abilityPickUp.abilityItem))
            {
                Debug.Log("Ability Added" + playerAbilitiesList.characterAbilities[playerAbilitiesList.characterAbilities.Count - 1].name);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pickedUpItem.boolState = false;
    }
}
