using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTemplateV2 : MonoBehaviour {

    public Button buttonComponent;
    public Text nameLabel;
    public Image iconImage;
    public Text priceText;

    private Items item;
    private ShopScrollList scrollList;

    // Use this for initialization
    void Start()
    {
        buttonComponent.onClick.AddListener(HandleClick);
    }

    public void Setup(Items currentItem, ShopScrollList currentList)
    {
        item = currentItem;
        nameLabel.text = item.itemName;
        iconImage.sprite = item.icon;
        priceText.text = item.price.ToString();
        scrollList = currentList;

    }

    public void HandleClick()
    {
        scrollList.TryTransferItemToOtherShop(item);
    }
}
