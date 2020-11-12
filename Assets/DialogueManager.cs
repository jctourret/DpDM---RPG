using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public static DialogueManager instance;
    public Queue<string> sentences;
    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }
    public void Start()
    {
        Queue<string> sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue) 
    {
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach(string element in dialogue.sentence)
        {
            sentences.Enqueue(element);
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }
    public void EndDialogue()
    {
        Debug.Log("End of conversation");
    }
}
