using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    Equipment[] currentEquipment;
    InventorySlot[] equipmentSlots;
    public Transform equipGrid;
    string currentScene;

    public delegate void OnEquipmentChange(Equipment newItem, Equipment oldItem);
    public OnEquipmentChange onEquipmentChangeCallback;

    Inventory inventory;

    public GameObject player;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        inventory = Inventory.instance;
        int equipSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[equipSlots];
        equipmentSlots = equipGrid.GetComponentsInChildren<InventorySlot>();
        DontDestroyOnLoad(gameObject);
    }
    public void Update()
    {
        if (currentScene != SceneManager.GetActiveScene().name)
        {
            player.GetComponent<PlayerController>().findControllers();

            currentScene = SceneManager.GetActiveScene().name;
        }
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
        equipmentSlots[slotIndex].AddItem(newItem);
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
            equipmentSlots[slotIndex].ClearSlot();
        }
    }
}
