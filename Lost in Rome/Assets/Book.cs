using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public GameObject questionMark;
    public GameObject bookUI; 
    private bool playerNear = false;

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            questionMark.SetActive(false);
            bookUI.SetActive(true);
            PlayerController.Instance.CanMove = false; 
        }

        if (bookUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            bookUI.SetActive(false);
            PlayerController.Instance.CanMove = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
        }
    }
}
