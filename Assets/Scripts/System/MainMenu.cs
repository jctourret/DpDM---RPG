using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene("Town"); 
    }
    public void OnCreditsButton()
    {
        
    }
    public void OnQuitButton()
    {
        Application.Quit();
    }
}
