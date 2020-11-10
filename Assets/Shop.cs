using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Interactable
{
    public Transform grid;
    public GameObject shopUI;
    Inventory inventory;
    InventorySlot[] slots;
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        slots = grid.GetComponentsInChildren<InventorySlot>();
    }
    public override void Interact()
    {
        base.Interact();
        shopUI.SetActive(!shopUI.activeSelf);
    }
}   
