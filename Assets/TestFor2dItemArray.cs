using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFor2dItemArray : MonoBehaviour {

    public int numberOfUpgradableItems;
    public List<Item> normalItem = new List<Item>();
    new LinkedList<Item> upgItem = new LinkedList<Item>();

    private void OnValidate()
    {
        normalItem = new List<Item>(numberOfUpgradableItems);
        upgItem = new LinkedList<Item>(normalItem);
        
    }

}
