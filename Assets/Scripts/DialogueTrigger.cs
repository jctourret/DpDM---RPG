using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Interactable
{
    public Dialogue dialogue;
    public bool isTalking;
    public override void Interact()
    {
        base.Interact();
        TriggerDialogue();
    }
    public void TriggerDialogue()
    {
        if (!isTalking)
        {
            isTalking = true;
            DialogueManager.instance.StartDialogue(this);
        }
        else
        {
            DialogueManager.instance.DisplayNextSentence(this);
        }
    }
}
