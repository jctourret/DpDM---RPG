using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactable
{
    public Item item;
    public override void Interact()
    {
        base.Interact();
        PickUp();
    }
    void PickUp()
    {
        if (Inventory.instance.AddToInv(item)) 
        {
            Destroy(gameObject);
            item = null;
        }
    }
}
