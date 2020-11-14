using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositMining : Interactable
{
    public Item mineral;
    PlayerStats playerStats;
    Animator playerAnim;
    int DepositContent = 100;
    private void Start()
    {
        playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        playerAnim = PlayerManager.instance.player.GetComponent<Animator>();
    }
    public override void Interact()
    {
        base.Interact();
        if(DepositContent > 0){
            MineDeposit();
            playerAnim.SetTrigger("attack");
        }
        else
        {
            PlayerManager.instance.gold += mineral.itemValue;
            PlayerManager.instance.showGold.updateGold();
            Destroy(gameObject);
        }
    }
    void MineDeposit()
    {
        DepositContent -= playerStats.miningRate.getValue();
    }
}
