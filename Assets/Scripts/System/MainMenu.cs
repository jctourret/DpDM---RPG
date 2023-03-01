using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Facebook.Unity;

public class MainMenu : MonoBehaviour
{
    public bool connectedToGooglePlay;
    public bool connectedToFacebook;
    private void Awake()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        FB.Init(SetInit,OnHideUnity);
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
        FB.LogInWithReadPermissions(permissions,AuthCallResult);
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
        SceneManager.LoadScene("Town"); 
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
