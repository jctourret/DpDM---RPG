using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Facebook.Unity;

public class GameOver : MonoBehaviour
{
    PlayerManager pm;
    string lossText = "The Blood debt was paid. All hope is lost.";
    string winText = "The Blood debt was paid. All hope is lost.";
    Text result;
    int screenTime = 15;
    public void Awake()
    {
        result = GetComponentInChildren<Text>();
    }
    IEnumerator GameOverEvent()
    {
        yield return new WaitForSeconds(screenTime);
        gameObject.SetActive(!gameObject.activeSelf);
        SceneManager.LoadScene("MainMenu");
    }
    public void ShowLoss()
    {
        result.text = lossText;
        result.color = Color.red;
        gameObject.SetActive(!gameObject.activeSelf);
        StartCoroutine(GameOverEvent());
    }

    public void ShowWin()
    {
        result.text = winText;
        result.color = Color.yellow;
        gameObject.SetActive(!gameObject.activeSelf);
        StartCoroutine(GameOverEvent());
    }
    public void FBShareFeed()
    {
        string url = "https://play.google.com/store/apps/details?id=com.CachuflitoGamesForever.BloodDebt";
        FB.ShareLink(new System.Uri(url), "I just finished Blood Oath!", "Here, why don't you check it out?", null, ShareCallback);
    }

    private static void ShareCallback(IShareResult result)
    {
        if(result.Error != null)
        {
            Debug.LogError(result.Error);
            return;
        }
    }
    public void BackToMenu()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        SceneManager.LoadScene("MainMenu");
    }
}
