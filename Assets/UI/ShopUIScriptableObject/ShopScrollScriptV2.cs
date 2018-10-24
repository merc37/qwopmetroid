using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScrollScriptV2 : MonoBehaviour {

    public BoolVariable shopedItem;

    public ShopList shopList;
    public Item item;

    public ShopList otherShopList;
    public ShopScrollScriptV2 otherShopScrollScript;

    public Transform contentPanel;
    public Text myGoldDisplay;
    public SimpleObjectPool buttonObjectPool;

    public Button buyButton;
    public Text itemDescription;
    public Text itemName;
    public Image itemSprite;

    public GameObject itemInfo;

    private bool canTransfer = true;

    // Use this for initialization
    void Start()
    {
        RefreshDisplay();
        buyButton.onClick.AddListener(BuyOrSellButton);
        //Debug.Log(shopList.gold.Value);
    }

    void RefreshDisplay()
    {
        myGoldDisplay.text = "Gold: " + shopList.gold.Value.ToString();
        RemoveButtons();
        AddButtons();
    }

    private void RemoveButtons()
    {
        //for (int i = 0; i < shopList.itemsList.Count; i++)
        //{
        //    Item item = shopList.itemsList[i];
        //    if (item.stackable == true && item.itemStack > 0)
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        while (contentPanel.childCount > 0)
        //        {
        //            GameObject toRemove = transform.GetChild(0).gameObject;
        //            buttonObjectPool.ReturnObject(toRemove);
        //            Debug.Log("button removed");
        //        }
        //    }
        //}

        while (contentPanel.childCount > 0)
        {
            GameObject toRemove = transform.GetChild(0).gameObject;
            buttonObjectPool.ReturnObject(toRemove);
            //Debug.Log("button removed");
        }

    }

    private void AddButtons()
    {
        for (int i = 0; i < shopList.itemsList.Count; i++)
        {
            Item item = shopList.itemsList[i];
            GameObject newButton = buttonObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel);
            newButton.transform.localScale = new Vector3(1, 1, 1);

            ButtonTemplate buttonTemplate = newButton.GetComponent<ButtonTemplate>();
            buttonTemplate.SetupItem(item/*,shopList*/, this);

            //int itemIndex = shopList.itemsList.IndexOf(item);

            //if (shopList.itemsList[itemIndex].stackable == true)
            //{
            //    GameObject newButton = buttonObjectPool.GetObject();
            //    newButton.transform.SetParent(contentPanel);
            //    newButton.transform.localScale = new Vector3(1, 1, 1);

            //    ButtonTemplate buttonTemplate = newButton.GetComponent<ButtonTemplate>();
            //    buttonTemplate.SetupItem(item/*,shopList*/, this);

            //}
            //else
            //{
            //    GameObject newButton = buttonObjectPool.GetObject();
            //    newButton.transform.SetParent(contentPanel);
            //    newButton.transform.localScale = new Vector3(1, 1, 1);

            //    ButtonTemplate buttonTemplate = newButton.GetComponent<ButtonTemplate>();
            //    buttonTemplate.SetupItem(item/*,shopList*/, this);
            //}
        }
    }

    public void TryTransferItemToOtherShop(Item item)
    {
        DisplaySellingItem(item);

        if (otherShopList.gold.Value >= item.itemCost && canTransfer)
        {
            shopList.gold.Variable.Value += item.itemCost;
            otherShopList.gold.Variable.Value -= item.itemCost;
            //otherShopScrollScript.shopList.gold.Variable.Value -= item.itemCost;

            AddItem(item, otherShopList);
            RemoveItem(item, shopList);

            shopedItem.boolState = true;

            RefreshDisplay();
            otherShopScrollScript.RefreshDisplay();
            //Debug.Log("enough gold");

        }
        else
        {
            //Debug.Log("attempted");
            shopedItem.boolState = false;
        }
        
    }

    void AddItem(Item itemToAdd, ShopList shopList)
    {
        //Debug.Log(itemToAdd.name + "added");
        if (shopList.itemsList.Contains(itemToAdd) && itemToAdd.stackable == true)
        {
            shopList.IncItemStackNumber(itemToAdd);
            Debug.Log("from Add stackable");
            //itemToAdd.itemStack++;
        }
        else
        {
            shopList.itemsList.Add(itemToAdd);
            shopList.AddItemToStack(itemToAdd);
            Debug.Log("from did not exist before");
        }
    }

    private void RemoveItem(Item itemToRemove, ShopList shopList)
    {
        //Debug.Log(itemToRemove.name + "removed");
        for (int i = shopList.itemsList.Count - 1; i >= 0; i--)
        {
            if (shopList.itemsList[i] == itemToRemove)
            {
                if (itemToRemove.stackable == true)
                {
                    if(shopList.DecItemStackNumber(itemToRemove)) {
                        float currentStackAmount = shopList.ItemAndStackNumber(item).y;
                        if(currentStackAmount <= 0) {
                            shopList.itemsList.RemoveAt(i);
                            shopList.RemoveItemFromStack(itemToRemove);
                            Debug.Log("from Remove stackable" + shopList.ItemAndStackNumber(itemToRemove).y);
                        }
                    }
                    //if (itemToRemove.itemStack > 1)
                    //{     
                    //    itemToRemove.itemStack--;
                    //    Debug.Log("itemonlystackamount changed" + item.itemName + item.itemStack.ToString());
                    //}
                    //else if(itemToRemove.itemStack <= 1)
                    //{
                    //    shopList.itemsList.RemoveAt(i);
                    //    Debug.Log("ItemRemoved From because stack 0 or 1");
                    //}
                }
                else
                {
                    Debug.Log("from normal");
                    shopList.itemsList.RemoveAt(i);
                    shopList.RemoveItemFromStack(itemToRemove);
                }
            }
        }
    }

    public void DisplaySellingItem(Item sellingItem)
    {
        //itemToSell = sellingItem;

        itemInfo.SetActive(true);

        if (sellingItem != null)
        {
            itemDescription.text = sellingItem.itemDescription;
            itemName.text = sellingItem.itemName;
            itemSprite.sprite = sellingItem.itemIcon;
        }

        if (item.currentShopIndex.Value != shopList.shopReferenceNumber.Value)
        {
            buyButton.GetComponentInChildren<Text>().text = "Sold to shop";
        }
        else
        {
            buyButton.GetComponentInChildren<Text>().text = "Bought from shop";
        }
        //Debug.Log("Displayed Item");
        
    }

    public void BuyOrSellButton()
    {
        //TryTransferItemToOtherShop(itemToSell);
        canTransfer = true;
    }
}
