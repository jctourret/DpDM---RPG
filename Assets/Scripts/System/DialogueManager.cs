using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text nameText;
    public Text dialogueText;
    int currentSentence;
    int conversationLength;
    public static DialogueManager instance;
    public List<string> sentences;
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
        sentences = new List<string>();
    }
    public void StartDialogue(DialogueTrigger trigger) 
    {
        dialoguePanel.SetActive(!dialoguePanel.activeSelf);
        nameText.text = trigger.dialogue.name;
        if (sentences != null)
        {
            sentences.Clear();
        }
        for (int i = 0; i < trigger.dialogue.sentence.Length; i++)
        {
            sentences.Add(trigger.dialogue.sentence[i]);
        }
        conversationLength = trigger.dialogue.sentence.Length;
        DisplayNextSentence(trigger);
    }
    public void DisplayNextSentence(DialogueTrigger trigger)
    {
        if (conversationLength == 0)
        {
            EndDialogue(trigger);
            return;
        }
        string sentence = sentences[currentSentence];
        sentences.Remove(sentences[currentSentence]);
        sentences.Add(sentence);
        dialogueText.text = sentence;
        conversationLength -= 1;
    }
    public void EndDialogue(DialogueTrigger trigger)
    {
        Debug.Log("End of conversation");
        trigger.isTalking = false;
        dialoguePanel.SetActive(!dialoguePanel.activeSelf);
    }
}
