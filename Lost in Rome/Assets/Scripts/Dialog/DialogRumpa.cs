using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogRumpa : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] TextMeshProUGUI dialogText;
    [SerializeField] int bokstäverPerSekund;
    
    public static DialogRumpa Instance { get; private set; }

    private Dialog dialog;
    private int currentRad = 0;
    private bool isTyping;
    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator VisaDialog(Dialog dialog)
    {
        EndDialog();
        this.dialog = dialog;
        currentRad = 0;
        dialogBox.SetActive(true);
        NextSentence();
        yield return null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isTyping)
            {
                NextSentence();
            }
            else
            {
                CompleteTypingCurrentLine();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) && dialogBox.activeInHierarchy)
        {
            EndDialog();
        }
    }

    void NextSentence()
    {
        if (currentRad < dialog.Rader.Count)
        {
            isTyping = true; 
            dialogText.text = ""; 
            StartCoroutine(TypeDialog(dialog.Rader[currentRad]));
            currentRad++;
        }
        else
        {
            EndDialog();
        }
    }

    private void CompleteTypingCurrentLine()
    {
        StopAllCoroutines();
        isTyping = false;
        dialogText.text = dialog.Rader[currentRad]; 
    }

    public IEnumerator TypeDialog(string rad)
    {
        isTyping = true;
        dialogText.text = "";
        foreach (var letter in rad.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / bokstäverPerSekund);
        }
        isTyping = false;
    }

    public void EndDialog()
    {
        StopAllCoroutines();
        dialogBox.SetActive(false);
        currentRad = 0; 
        isTyping = false; 
        PlayerController.Instance.CanMove = true; 
    }
}
