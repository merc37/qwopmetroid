using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonTemplate : MonoBehaviour
{
    public Button buttonComponent;
    public Text nameLabel;
    public Image iconImage;
    public Text priceText;
    public Text stackItem;

    internal bool itemIsStackable;
    private float price;

    public Item itemToSell;
    public Item duplicateItem;

    internal ShopScrollScriptV2 scrollList;
    //private ItemInfoScript infoDisplay;

    public ButtonTemplate thisButtom;
    //public ItemInfoScript itemInfo;
    //private ShopScrollScriptV2 scrollList;

    // Use this for initialization
    void Start()
    {
        buttonComponent.onClick.AddListener(HandleClick);
        itemIsStackable = false;
        stackItem.enabled = false;
    }

    public void SetupItem(Item currentItem,/* ShopList currentShop,*/ShopScrollScriptV2 currentShop)
    {
        duplicateItem = currentItem;

        if (duplicateItem.stackable == true)
        {
            itemIsStackable = true;
            stackItem.text = duplicateItem.itemStack.ToString();
            stackItem.enabled = true;
        }
        else
        {
            itemIsStackable = false;
            stackItem.enabled = false;
        }
        nameLabel.text = duplicateItem.itemName;
        iconImage.sprite = duplicateItem.itemIcon;
        priceText.text = duplicateItem.itemCost.ToString();
        price = duplicateItem.itemCost;
        
        //duplicateItem.currentShopIndex.Variable.Value = currentShop.shopList.shopReferenceNumber.Value;
        scrollList = currentShop;
    }


    public void SetItemToBeSold(Item item)
    {
        item = duplicateItem;
        item.name = duplicateItem.itemName;
        item.itemIcon = duplicateItem.itemIcon;
        item.itemCost = duplicateItem.itemCost;
        //item.currentShopIndex.Variable.Value = duplicateItem.currentShopIndex.Value;
        //Debug.Log("ItemSet");
    }

    public void HandleClick()
    {
        SetItemToBeSold(itemToSell);

        ////Debug.Log(thisButtom.duplicateItem.itemName);
        scrollList.DisplaySellingItem(duplicateItem);
        scrollList.TryTransferItemToOtherShop(duplicateItem);
    }
}