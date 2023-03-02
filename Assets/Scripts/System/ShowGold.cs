using UnityEngine;
using UnityEngine.UI;

public class ShowGold : MonoBehaviour
{
    Text gold;
    void Awake()
    {
        gold = GetComponent<Text>();
        int a = 1;
    }
    public void updateGold()
    {
        if(gold == null)
        {
            gold = GetComponent<Text>();
        }
        gold.text = PlayerManager.instance.gold.ToString();
    }
}
