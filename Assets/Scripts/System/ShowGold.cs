using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowGold : MonoBehaviour
{
    Text gold;
    void Start()
    {
        gold = GetComponent<Text>();
    }
    public void updateGold()
    {
        gold.text = PlayerManager.instance.gold.ToString();
    }
}
