using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private void Start()
    {
        PlayerManager.instance.onEquipmentChangeCallback += OnEquipmentChange;
    }
    void OnEquipmentChange(Equipment newItem,Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.addModifier(newItem.armorMod);
            damage.addModifier(newItem.damageMod);
            miningRate.addModifier(newItem.miningMod);
        }
        if(oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorMod);
            damage.RemoveModifier(oldItem.damageMod);
            miningRate.RemoveModifier(oldItem.miningMod);
        }
    }

    public override void Die()
    {
        base.Die();
        PlayerManager.instance.KillPlayer();
    }
}
