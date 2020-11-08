using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    public delegate void OnInventoryChange();
    public OnInventoryChange onInventoryChangeCallback;
    public int space = 4;
    public List<Item> inventory = new List<Item>();
    public bool AddToInv(Item item)
    {
        if (item != null)
        {
            if (!item.defaultItem)
            {
                if (inventory.Count > space)
                {
                    Debug.Log("Inventory Full");
                    return false;
                }
                inventory.Add(item);
                if (onInventoryChangeCallback != null)
                {
                    onInventoryChangeCallback.Invoke();
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    public void RemoveFromInv(Item item)
    {
        inventory.Remove(item);
        if (onInventoryChangeCallback != null)
        {
            onInventoryChangeCallback.Invoke();
        }
    }
}
