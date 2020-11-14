using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverLose : MonoBehaviour
{
    PlayerManager pm;
    int screenTime= 5;
    IEnumerator GameOverEvent()
    {
        yield return new WaitForSeconds(screenTime);
        gameObject.SetActive(!gameObject.activeSelf);
        SceneManager.LoadScene("MainMenu");
    }
    public void ShowGameOver()
    {
         gameObject.SetActive(!gameObject.activeSelf);
         StartCoroutine(GameOverEvent());
    }
}
