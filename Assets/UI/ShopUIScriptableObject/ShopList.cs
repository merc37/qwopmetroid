using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Shop List", menuName = "ScriptableObjects/ShopList", order = 2)]
public class ShopList : ScriptableObject {

    public List<Item> itemsList = new List<Item>();

    public FloatReference gold;
    public FloatReference shopReferenceNumber;

    [SerializeField] Vector2[] itemAndStackNumber = new Vector2[20];

    public void ClearAndAddItemData()
    {
        ClearItemAndStackRef();
        AddItemFromItemList();
    }

    private void ClearItemAndStackRef()
    {
        for (int i = 0; i < itemAndStackNumber.Length; i++)
        {
            itemAndStackNumber[i] = Vector2.zero;
        }
    }

    private void AddItemFromItemList()
    {
        for (int i = 0; i < itemsList.Count; i++)
        {
            itemAndStackNumber[i] = new Vector2(itemsList[i].itemNumber, 1);
        }
    }

    public Vector2 ItemAndStackNumber(Item item)
    {
        for (int i = 0; i < itemAndStackNumber.Length; i++)
        {
            if (itemAndStackNumber[i].x == item.itemNumber)
            {
                return itemAndStackNumber[i];
            }
        }
        return Vector2.zero;
    }

    public bool ContainsItemInStack(Item item)
    {
        for (int i = 0; i < itemAndStackNumber.Length; i++)
        {
            if (itemAndStackNumber[i].x == item.itemNumber)
            {
                return true;
            }
        }
        return false;
    }

    public bool AddItemToStack(Item item)
    {
        for (int i = 0; i < itemAndStackNumber.Length; i++)
        {
            if(itemAndStackNumber[i].x == 0)
            {
                itemAndStackNumber[i].x = item.itemNumber;
                itemAndStackNumber[i].y = 1;
                return true;
            }
        }
        return false;
    }

    public bool RemoveItemFromStack(Item item)
    {
        for (int i = itemAndStackNumber.Length - 1; i >= 0; i--)
        {
            if (itemAndStackNumber[i].x == item.itemNumber)
            {
                itemAndStackNumber[i] = Vector2.zero;
                return true;
            }
        }
        return false;
    }

    public bool IncItemStackNumber(Item item)
    {
        for (int i = 0; i < itemAndStackNumber.Length; i++)
        {
            if(itemAndStackNumber[i].x == item.itemNumber)
            {
               itemAndStackNumber[i].y++;
                return true;
            }
        }
        return false;
    }

    public bool DecItemStackNumber(Item item)
    {
        for (int i = 0; i < itemAndStackNumber.Length; i++)
        {
            if (itemAndStackNumber[i].x == item.itemNumber)
            {
                itemAndStackNumber[i].y--;
                return true;
            }
        }
        return false;
    }
}
