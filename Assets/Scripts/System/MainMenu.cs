using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Facebook.Unity;

public class MainMenu : MonoBehaviour
{
    public bool connectedToGooglePlay;
    public bool connectedToFacebook;

    Text friends;

    public TextMeshProUGUI friendsText;
    private void Awake()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        if (!FB.IsInitialized)
        {
            FB.Init(SetInit, OnHideUnity);
        }
    }
    private void Start()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    void SetInit()
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("Login successful");
            FB.ActivateApp();
        }
        else
        {
            Debug.Log("Login failed");
        }
    }

    void OnHideUnity(bool gameShown)
    {
        if (gameShown)
        {
            Time.timeScale = 1;
            FB.ActivateApp();
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    public void FbLogin()
    {
        List<string> permissions = new List<string>();
        permissions.Add("public_profile");
        permissions.Add("email");
        permissions.Add("user_friends");
        FB.LogInWithReadPermissions(permissions,AuthCallResult);
    }

    public void FbLogout()
    {
        FB.LogOut();
    }

    public void FacebookGameRequest()
    {
        FB.AppRequest("Hey! Come and play this awesome game!",title: "Blood Oath");
    }

    public void GetFriendsPlayingGame()
    {
        string query = "/me/friends";
        FB.API(query, HttpMethod.GET, result =>
        {
            var dictionary = (Dictionary<string, object>)Facebook.MiniJSON.Json.Deserialize(result.RawResult);
            var friendlist = (List<object>)dictionary["data"];
            friendsText.text = string.Empty;
            friendsText.text = "Friends playing: ";
            foreach (var dict in friendlist)
            {
                friendsText.text += ((Dictionary<string, object>)dict)["name"];
            }
        }
        );
    }
    public void FBShareFeed()
    {
        string url = "https://play.google.com/store/apps/details?id=com.CachuflitoGamesForever.BloodDebt";
        FB.ShareLink(new System.Uri(url), "I just finished Blood Oath!", "Here, why don't you check it out?", null, ShareCallback);
    }
    private static void ShareCallback(IShareResult result)
    {
        if (result.Error != null)
        {
            Debug.LogError(result.Error);
            return;
        }
    }
    void AuthCallResult(ILoginResult result)
    {
        if(result.Error != null)
        {
            Debug.Log(result.Error);
        }
        else
        {
            if (FB.IsLoggedIn)
            {
                Debug.Log("Manual Login sucessful");
                Debug.Log(result.RawResult);
                connectedToFacebook = true;
            }
            else
            {
                Debug.Log("Manual Login failed");
                connectedToFacebook = false;
            }
        }
    }
    public void OnPlayButton()
    {
        SceneManager.LoadSceneAsync("Town"); 
    }
    public void OnQuitButton()
    {
        Application.Quit();
    }


    public void ShowLeaderBoard()
    {
        Social.ShowLeaderboardUI();
    }
    public void ShowAchievementUI()
    {
        Social.ShowAchievementsUI();
    }
    private void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            connectedToGooglePlay = true;
        }
        else
        {
            connectedToGooglePlay = false;
        }
    }
    
}
