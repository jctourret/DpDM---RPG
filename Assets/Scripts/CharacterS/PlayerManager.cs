using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

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
    public delegate void OnWeekEnd();
    public OnWeekEnd onWeekEndCallback;

    Inventory inventory;

    public GameObject player;
    PlayerStats stats;
    public ShowCurrentDay dayPanel;
    public GameOver GameOverPanel;
    public ShowGold showGold;
    public float gold;
    public int playerDebt = 1000;
    public int currentDay = 0;
    int weekLenght = 7;

    public bool connectedToGooglePlay;
    bool hasEnteredDungeon = false;
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
        stats = player.GetComponent<PlayerStats>();
        inventory = Inventory.instance;
        int equipSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[equipSlots];
        equipmentSlots = equipGrid.GetComponentsInChildren<InventorySlot>();
        showGold.updateGold();
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
                Social.ReportScore((long)gold,GPGSIds.leaderboard_blood_oath,LeaderboardUpdate);
                
                FindObjectOfType<AudioManager>().Play("Town");
                AudioManager.instance.Play("Daypass");
                stats.currentHealth = stats.maxHealth;
                currentDay += 1;
                dayPanel.ShowDay();
                if (currentDay > weekLenght)
                {
                    if (playerDebt > gold)
                    {
                        GameOverPanel.ShowLoss();
                        resetGame();
                    }
                    else
                    {
                        GameOverPanel.ShowWin();
                        AudioManager.instance.Play("Victory");
                        resetGame();
                    }
                }
            }
            if(currentScene == "Dungeon")
            {
                AudioManager.instance.Play("Dungeon");
                if (!hasEnteredDungeon)
                {
                    PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_enter_the_dungeon,100.0f,(result)=>
                    {
                        if (result)
                        {
                            Debug.Log("Progress reported.");
                        }
                        else
                        {
                            Debug.LogWarning("Failed to report progress.");
                        }
                    });
                    hasEnteredDungeon = true;
                }
            }
            else
            {
                AudioManager.instance.Stop("Dungeon");
            }
        }
    }
    private void LeaderboardUpdate(bool success)
    {
        if (success)
        {
            Debug.Log("Updated Leaderboard");
        }
        else
        {
            Debug.Log("Unable to Update Leaderboard");
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
        gold = 0f;
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
