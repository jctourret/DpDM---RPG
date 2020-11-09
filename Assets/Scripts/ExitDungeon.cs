using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDungeon : Interactable
{
    public override void Interact()
    {
        base.Interact();
        SceneManager.LoadScene("Town");
    }
}
