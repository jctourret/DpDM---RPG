using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : Item
{
    public override void Use()
    {
        base.Use();
        if (isInShop)
        {
            PlayerManager.instance.gold += itemValue;
            PlayerManager.instance.onGoldChangeCallback.Invoke();
        }
    }

}
