using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoDungeon : Interactable
{
    public override void Interact()
    {
        base.Interact();
        SceneManager.LoadScene("Dungeon");
    }
}
