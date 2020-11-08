using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositMining : Interactable
{
    public Rigidbody mineral;
    CharacterStats playerStats;
    int DepositContent = 100;
    float lootForce = 5f;
    int minDrop = 1;
    int maxDrop = 2;
    private void Start()
    {
        playerStats = PlayerManager.instance.player.GetComponent<CharacterStats>();
    }
    public override void Interact()
    {
        base.Interact();
        if(DepositContent > 0) {
            MineDeposit();
        }
        else
        {
            int random = Random.Range(minDrop,maxDrop);
            Rigidbody mineralIns;
            for (int i = 0; i == random; i++)
            {
                mineralIns = Instantiate(mineral,gameObject.transform.position,gameObject.transform.rotation) as Rigidbody;
                mineralIns.AddForce(gameObject.transform.up * lootForce);
            }
            Destroy(gameObject);
        }
    }
    void MineDeposit()
    {
        DepositContent -= playerStats.miningRate.getValue();
    }
}
