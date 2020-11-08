using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    Equipment[] currentEquipment;

    public delegate void OnEquipmentChange(Equipment newItem, Equipment oldItem);
    public OnEquipmentChange onEquipmentChangeCallback;

    Inventory inventory;

    public GameObject player;

    private void Start()
    {
        inventory = Inventory.instance;
        int equipSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[equipSlots];
    }

    public void KillPlayer()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipslot;
        Equipment oldItem = null;
        if(currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.AddToInv(oldItem);
        }

        if (onEquipmentChangeCallback != null)
        {
            onEquipmentChangeCallback.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;
    }
    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.AddToInv(oldItem);
             
            if (onEquipmentChangeCallback != null)
            {
                onEquipmentChangeCallback.Invoke(null, oldItem);
            }

            currentEquipment[slotIndex] = null;
        }
    }
}
