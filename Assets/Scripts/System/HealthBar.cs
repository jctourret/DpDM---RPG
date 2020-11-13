using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    PlayerStats player;
    private void Start()
    {
        player = PlayerManager.instance.player.GetComponent<PlayerStats>();
    }
    private void Update()
    {
        slider.value = player.currentHealth;
    }
}
