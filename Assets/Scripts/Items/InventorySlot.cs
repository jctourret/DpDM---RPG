using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    Item currentItem;    
    public void AddItem(Item newItem)
    {
        currentItem = newItem;
        icon.sprite = currentItem.icon;
        Debug.Log("Should activate icon");
        icon.enabled = true;
        removeButton.interactable = true;
    }
    public void ClearSlot() {
        currentItem = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }
    public void OnRemoveButton()
    {
        Inventory.instance.RemoveFromInv(currentItem);
    }
    public void UseItem()
    {
        if (currentItem != null) {
            currentItem.Use();
        }
    }
}
