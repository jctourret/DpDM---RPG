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
        base.Use();
        PlayerManager.instance.Equip(this);
        RemoveFromInventory();
    }
}
public enum EquipmentSlot { Head, Chest, Leg, Tool, Weapon, Shield, Feet}