using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "Item Name";
    public Sprite icon = null;
    public bool defaultItem = false;
    public bool isInShop = false;
    public int itemValue;
    public virtual void Use()
    {
        Debug.Log("Using" + name);

    }
    public virtual bool Buy()
    {
        return false;
    }
    public void RemoveFromInventory()
    {
        Inventory.instance.RemoveFromInv(this);
    }
}
