using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInv : MonoBehaviour
{
    public static ShopInv instance;
    public delegate void OnShopChange();
    public OnShopChange onShopChangeCallback;
    public int space = 9;
    public List<Item> shop = new List<Item>();
    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public bool AddToInv(Item item)
    {
        if (item != null)
        {
            if (!item.defaultItem)
            {
                if (shop.Count > space)
                {
                    Debug.Log("Inventory Full");
                    return false;
                }
                shop.Add(item);
                if (onShopChangeCallback != null)
                {
                    onShopChangeCallback.Invoke();
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    public void RemoveFromShop(Item item)
    {
        shop.Remove(item);
        if (onShopChangeCallback != null)
        {
            onShopChangeCallback.Invoke();
        }
    }
}
