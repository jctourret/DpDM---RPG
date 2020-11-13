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
            Debug.Log("Item no es nulo");
            if (!item.defaultItem)
            {
                Debug.Log("Item no es default");
                if (inventory.Count > space)
                {
                    Debug.Log("Inventory Full");
                    return false;
                }
                Debug.Log("Agregar Item");
                inventory.Add(item);
                Debug.Log("UpdateUI");
                InventoryUI.instance.updateUI();
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
        Debug.Log("Item is being removed");
        inventory.Remove(item);
        Instantiate(item,PlayerController.instance.transform);
        if (onInventoryChangeCallback != null)
        {
            onInventoryChangeCallback.Invoke();
        }
    }
}
