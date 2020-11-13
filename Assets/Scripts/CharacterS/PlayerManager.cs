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
    public string currentScene;

    public delegate void OnEquipmentChange(Equipment newItem, Equipment oldItem);
    public OnEquipmentChange onEquipmentChangeCallback;
    public delegate void OnGoldChange();
    public OnGoldChange onGoldChangeCallback;
    public delegate void OnDayChange();
    public OnDayChange onDayChangeCallback;
    public delegate void OnWeekEnd();
    public OnWeekEnd onWeekEndCallback;

    Inventory inventory;

    public GameObject player;
    public ShowCurrentDay dayPanel;
    public GameOverLose losePanel;
    public GameOverWin winPanel;
    public float gold;
    public int playerDebt = 1000;
    public int currentDay = 0;
    int weekLenght = 7;

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
            PlayerController.instance.findControllers();
            currentScene = SceneManager.GetActiveScene().name;
            if (currentScene == "Town")
            {
                AudioManager.instance.Play("Town");
                AudioManager.instance.Play("Daypass");
                currentDay += 1;
                dayPanel.ShowDay();
                if (currentDay > weekLenght)
                {
                    if (playerDebt > gold)
                    {
                        losePanel.ShowGameOver();
                        resetGame();
                    }
                    else
                    {
                        winPanel.ShowGameOver();
                        AudioManager.instance.Play("Victory");
                        resetGame();
                    }
                }
            }
            if(currentScene == "Dungeon")
            {
                AudioManager.instance.Play("Dungeon");
            }
        }
    }
    public void resetGame()
    {
        Debug.Log("reset");
        gold = 0;
        currentDay = 0; 
        inventory.inventory.Clear();
        for (int i =0; i < System.Enum.GetNames(typeof(EquipmentSlot)).Length; i++)
        {
            Unequip(i);
        }
        inventory.inventory.Clear();
    }
    public void KillPlayer()
    {
        AudioManager.instance.Play("Death");
        SceneManager.LoadScene("Town");
        currentDay += 1;
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
