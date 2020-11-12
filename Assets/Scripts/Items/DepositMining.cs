using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositMining : Interactable
{
    public GameObject mineral;
    PlayerStats playerStats;
    int DepositContent = 100;
    private void Start()
    {
        playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
    }
    public override void Interact()
    {
        base.Interact();
        if(DepositContent > 0){
            MineDeposit();
        }
        else
        {
            Instantiate(mineral, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    void MineDeposit()
    {
        DepositContent -= playerStats.miningRate.getValue();
    }
}
