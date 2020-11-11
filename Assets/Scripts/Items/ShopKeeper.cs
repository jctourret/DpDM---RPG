﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : Interactable
{
    public ShopUI shopUI;
    Inventory playerInv;
    // Start is called before the first frame update
    void Start()
    {
        playerInv = Inventory.instance;
        shopUI = ShopUI.instance;
    }
    private void Update()
    {
        if (PlayerController.instance.interacting && Vector3.Distance(gameObject.transform.position, PlayerController.instance.transform.position) > 10) {
            if (shopUI.shopUI.activeInHierarchy)
            {
                shopUI.shopUI.SetActive(!shopUI.shopUI.activeSelf);
            }
        };
    }
    public override void Interact()
    {
        base.Interact();
        foreach (Item element in playerInv.inventory)
        {
            element.isInShop = !element.isInShop;
        }
        shopUI.updateUI();
        shopUI.shopUI.SetActive(!shopUI.shopUI.activeSelf);
    }
}
