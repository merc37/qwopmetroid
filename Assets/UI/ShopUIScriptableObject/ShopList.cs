using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Shop List", menuName = "ScriptableObjects/ShopList", order = 2)]
public class ShopList : ScriptableObject {

    public List<Item> itemsList = new List<Item>();

    public FloatReference gold;
    public FloatReference shopReferenceNumber;

}
