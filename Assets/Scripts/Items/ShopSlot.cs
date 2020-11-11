using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public Image icon;
    public Item currentItem;
    public void AddItem(Item newItem)
    {
        currentItem = newItem;
        icon.sprite = currentItem.icon;
        Debug.Log("Should activate icon");
        icon.enabled = true;
    }
    public void ClearSlot()
    {
        currentItem = null;
        icon.sprite = null;
        icon.enabled = false;
    }
    public void BuyItem()
    {
        if (currentItem != null)
        {
            if (currentItem.Buy())
            {
                ShopInv.instance.RemoveFromShop(currentItem);
            }
        }
    }
}
