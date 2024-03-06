using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private List<dialogueString> dialogueStrings = new List<dialogueString>();
    [SerializeField] private Transform NPCTransform;

    private bool hasSpoken = false;

    //När spelaren kolliderar med triggern som är boxen innan NPC
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !hasSpoken)
        {
            collision.GetComponent<DialogueManager>().DialogueStart(dialogueStrings, NPCTransform);
            hasSpoken = true;
        }
    }
}

[System.Serializable]
public class dialogueString
{   
    
    public string text; //Detta representerar vad NPC ska säga
    public bool isEnd; //Detta representerar ifall det är den sista replik i konversationen

    //När vi skapar dialog så har vi flertalet alternativ vi kan välja på
    [Header("Branch")]
    public bool isQuestion;
    public string answerOption1;
    public string answerOption2;
    public int option1IndexJump;
    public int option2IndexJump;

    //Gör att vi kan lägga till effekter eller liknande
    [Header("Triggered Events")]
    public UnityEvent startDialogueEvent;
    public UnityEvent endDialogueEvent;

}
