using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogRumpa : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] TextMeshProUGUI dialogText;
    [SerializeField] int bokst�verPerSekund;

    public static DialogRumpa Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    Dialog dialog;
    int currentRad = 0;
    bool isTyping;

    public IEnumerator VisaDialog(Dialog dialog)
    {
        yield return new WaitForEndOfFrame();

        this.dialog = dialog;
        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Rader[0]));
    }

    private void Update()
    {
        HandleUpdate();
        if(Input.GetKeyDown(KeyCode.Escape) && dialogBox.activeInHierarchy)
        {
            CancelDialog();
        }
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isTyping)
            {
                ++currentRad;
                if (currentRad < dialog.Rader.Count)
                {
                    StartCoroutine(TypeDialog(dialog.Rader[currentRad]));
                }
                else
                {
                    currentRad = 0;
                    dialogBox.SetActive(false);
                    PlayerController.Instance.CanMove = true; // L�t spelaren r�ra sig igen
                }
            }
            else
            {
                // Snabba fram till slutet av nuvarande rad om spelaren trycker p� "E" under utskrift
                StopAllCoroutines(); // Stoppar den nuvarande korutinen
                dialogText.text = dialog.Rader[currentRad]; // Visar hela raden direkt
                isTyping = false; // S�tter isTyping till false s� att n�sta rad kan b�rja
            }
        }
    }

    public void CancelDialog()
    {
        StopAllCoroutines();
        dialogBox.SetActive(false);
        PlayerController.Instance.CanMove = true;
    }

    public IEnumerator TypeDialog(string dialog)
    {
        isTyping = true;
        dialogText.text = "";
        foreach(var bokstav in dialog.ToCharArray())
        {
            dialogText.text += bokstav;
            yield return new WaitForSeconds(1f / bokst�verPerSekund);
        }
        isTyping = false;
    }
}
