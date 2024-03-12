using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueParent;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Button option1Button;
    [SerializeField] private Button option2Button;
    

    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private float turningSpeed = 2f;

    private List<dialogueString> dialogueList;

    [Header("Player")]
    private Transform playerCamera;

    private int currentDialogueIndex = 0;

    private void Start()
    {
        //St�nger av canvasen
        dialogueParent.SetActive(false);
        playerCamera = Camera.main.transform;
       
    }

    public void DialogueStart(List<dialogueString> textToPrint, Transform NPC)
    {
        dialogueParent.SetActive(true);

        //G�r s� att man kan anv�nda musen
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //Metod f�r att vrida kameran mot NPC
        StartCoroutine(TurnCameraTowardsNPC(NPC));


        dialogueList = textToPrint;
        currentDialogueIndex = 0;

        //St�nger av knapparna n�r man inte kan svara
        DisableButtons();

        //Skriver ut dialogen
        StartCoroutine(PrintDialogue());
    }

    private void DisableButtons()
    {
        //St�nger av knapparna
        option1Button.interactable = false;
        option2Button.interactable = false;

        //Skriver p� knappar no option n�r man inte kan svara
        option1Button.GetComponentInChildren<TMP_Text>().text = "No Option";
        option2Button.GetComponentInChildren<TMP_Text>().text = "No Option";
    }

    private IEnumerator TurnCameraTowardsNPC(Transform NPC)
    {
        //D�r player kameran �r just nu
        Quaternion startRotation = playerCamera.rotation;
        //Tar fram vart n�gonstans kameran ska flytta sig till vilket �r NPC position
        Quaternion targetRotation = Quaternion.LookRotation(NPC.position - playerCamera.position);

        //Hur l�ng tid det ska ta f�r kameran att snappa dit den ska
        float elapsedTime = 0f;

        while(elapsedTime < 1f)
        {
            playerCamera.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime);
            elapsedTime += Time.deltaTime * turningSpeed;
            yield return null;
        }
        //Flytar player kameran mot NPC
        playerCamera.rotation = targetRotation;
    }

    private bool optionSelected = false;

    private IEnumerator PrintDialogue()
    {
        while (currentDialogueIndex < dialogueList.Count)
        {
            dialogueString line = dialogueList[currentDialogueIndex];

            //G�r att vi kan s�tta p� effekter vid dialog
            line.startDialogueEvent?.Invoke();

            if (line.isQuestion)
            {
                yield return StartCoroutine(typeText(line.text));

                //S�tter p� knapparna
                option1Button.interactable = true;
                option2Button.interactable = true;

                //Skriver p� knappar vad vi vill svara med
                option1Button.GetComponentInChildren<TMP_Text>().text = line.answerOption1;
                option2Button.GetComponentInChildren<TMP_Text>().text = line.answerOption2;

                //Lyssnar efter vilket alternativ man klickat p�
                option1Button.onClick.AddListener(() => HandleOptionSelected(line.option1IndexJump));
                option2Button.onClick.AddListener(() => HandleOptionSelected(line.option2IndexJump));

                yield return new WaitUntil(() => optionSelected);
            }
            else
            {
                yield return StartCoroutine(typeText(line.text));
            }

            //Slutar dialogen
            line.endDialogueEvent?.Invoke();

            optionSelected = false;
        }

        DialogueStop();
    }

    //Tar hand om vilken knapp man tryckt p�
    private void HandleOptionSelected(int indexJump)
    {
        optionSelected = true; // S�g i kommentarerna p� videon att detta kanske beh�ver �ndras till true
        DisableButtons();

        currentDialogueIndex = indexJump;
    }

    //Skriver ut texten f�r dialogen
    private IEnumerator typeText(string text)
    {
        dialogueText.text = "";
        foreach(char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        if (!dialogueList[currentDialogueIndex].isQuestion)
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }

        if (dialogueList[currentDialogueIndex].isEnd)
        {
            DialogueStop();
        }

        currentDialogueIndex++;
    }

    private void DialogueStop()
    {
        StopAllCoroutines();
        dialogueText.text = "";
        dialogueParent.SetActive(false);

        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


}
