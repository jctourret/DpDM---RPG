using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowGold : MonoBehaviour
{
    Text gold;
    PlayerManager pm;
    void Start()
    {
        gold = GetComponent<Text>();
        pm = PlayerManager.instance;
        pm.onGoldChangeCallback += updateGold;
    }

    
    public void updateGold()
    {
        gold.text = pm.gold.ToString();
    }
}
