using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCurrentDay : MonoBehaviour
{
    public Text day;
    Image image;
    PlayerManager pm;
    public float opacity;
    float opacityDrop = 1f;
    float maxOpacity = 1;
    private void Start()
    {
        image = GetComponent<Image>();
        day = GetComponentInChildren<Text>();
        pm = PlayerManager.instance;
        pm.onDayChangeCallback += ShowDay;
    }
    IEnumerator ShowDayEvent()
    {
        for(opacity=maxOpacity; opacity > 0; opacity -=  Time.deltaTime *opacityDrop )
        {
            day.color = new Color(day.color.b, day.color.g, day.color.b, opacity);
            image.color = new Color(image.color.b, image.color.g, image.color.b, opacity);
            yield return null;
        }
        gameObject.SetActive(!gameObject.activeSelf);
    }
    public void ShowDay()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        image = GetComponent<Image>();
        day = GetComponentInChildren<Text>();
        opacity = maxOpacity;
        day.text = "Day " + PlayerManager.instance.currentDay.ToString();
        day.color = new Color(day.color.r, day.color.g, day.color.b, maxOpacity);
        image.color = new Color(image.color.r, image.color.g, image.color.b, maxOpacity);
        StartCoroutine(ShowDayEvent());
    }
}
