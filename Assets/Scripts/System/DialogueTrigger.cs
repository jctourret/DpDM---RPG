using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Interactable
{
    public Dialogue dialogue;
    public bool isTalking;
    public float maxDistance = 3f;
    public override void Interact()
    {
        base.Interact();
        TriggerDialogue();
    }
    public void Update()
    {
        if (isTalking && Vector3.Distance(gameObject.transform.position, PlayerController.instance.transform.position) > maxDistance)
        {
            DialogueManager.instance.EndDialogue(this);
        }
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
