using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryUI;
    public Transform grid;

    Inventory inventory;
    InventorySlot[] slots;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onInventoryChangeCallback += updateUI;
        slots = grid.GetComponentsInChildren<InventorySlot>();
        
    }
    void updateUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.inventory.Count)
            {
                slots[i].AddItem(inventory.inventory[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
    public void OnInventoryButton()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }
}
