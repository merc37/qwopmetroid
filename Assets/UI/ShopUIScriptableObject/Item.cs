using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Item", order = 3)]
public class Item : ScriptableObject {

    public string itemName;
    public float itemCost;
    public Sprite itemIcon;
    public string itemDescription;

    public int itemNumber;
    public float itemStack;
    public bool stackable;

    public FloatReference currentShopIndex;
}
