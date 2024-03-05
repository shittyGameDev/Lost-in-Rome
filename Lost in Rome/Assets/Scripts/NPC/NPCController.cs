using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] Dialog dialog;
    bool playerIsNear = false;

    private void Update()
    {
        if(playerIsNear && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }
    public void Interact()
    {
        StartCoroutine(DialogRumpa.Instance.VisaDialog(dialog));
        PlayerController.Instance.CanMove = false;
    }
}
