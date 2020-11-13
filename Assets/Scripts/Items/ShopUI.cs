using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public static ShopUI instance;
    public GameObject shopUI;
    public Transform grid;

    ShopInv shopInv;
    ShopSlot[] slots;

    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        shopInv = ShopInv.instance;
        if (shopInv != null)
        {
            shopInv.onShopChangeCallback += updateUI;
        }
        slots = grid.GetComponentsInChildren<ShopSlot>();
        DontDestroyOnLoad(gameObject);
    }
    public void updateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < shopInv.shop.Count && shopInv.shop[i] != null)
            {
                slots[i].AddItem(shopInv.shop[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
