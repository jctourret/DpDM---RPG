using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;
    public GameObject inventoryUI;
    public Transform grid;

    Inventory inventory;
    InventorySlot[] slots;

    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        inventory = Inventory.instance;
        inventory.onInventoryChangeCallback += updateUI;
        slots = grid.GetComponentsInChildren<InventorySlot>();
        DontDestroyOnLoad(gameObject);
    }
    public void updateUI()
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
