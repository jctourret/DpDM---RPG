﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : Interactable
{
    public ShopUI shopUI;
    int maxShopRange=3;
    Inventory playerInv;
    // Start is called before the first frame update
    void Start()
    {
        playerInv = Inventory.instance;
        shopUI = ShopUI.instance;
    }
    private void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, PlayerController.instance.transform.position) > maxShopRange) {
            if (shopUI.shopUI.activeInHierarchy)
            {
                shopUI.shopUI.SetActive(!shopUI.shopUI.activeSelf);
                foreach (Item element in playerInv.inventory)
                {
                    element.isInShop = !element.isInShop;
                }
#if UNITY_STANDALONE || UNITY_EDITOR
                Cursor.lockState = CursorLockMode.Confined;
                PlayerController.instance.isInShop = false;
#endif
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
#if UNITY_STANDALONE || UNITY_EDITOR
        Cursor.lockState = CursorLockMode.None;
        PlayerController.instance.isInShop = true;
#endif
    }
}
