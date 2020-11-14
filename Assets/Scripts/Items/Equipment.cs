using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment",menuName ="Inventory/Equipment")] 
public class Equipment : Item
{
    public EquipmentSlot equipslot;
    public int armorMod;
    public int damageMod;
    public int miningMod;
    public override void Use()
    {
        if (!isInShop)
        {
            PlayerManager.instance.Equip(this);
            RemoveFromInventory();
        }
        else
        {
            PlayerManager.instance.gold += itemValue;
            Debug.Log("Should remove.");
            PlayerManager.instance.showGold.updateGold();
            RemoveFromInventory();
        }
    }
    public override bool Buy()
    {
        if(PlayerManager.instance.gold >= itemValue)
        {
            PlayerManager.instance.gold -= itemValue;
            PlayerManager.instance.showGold.updateGold();
            Inventory.instance.AddToInv(this);
            return true;
        }
        return false;
    }
}
public enum EquipmentSlot { Head, Chest, Leg, Tool, Weapon, Hands, Feet}