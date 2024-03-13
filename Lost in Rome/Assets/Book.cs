using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public GameObject questionMark;
    public GameObject[] bookPages; // Array av sidor
    private bool playerNear = false;
    public static bool hasReadBook = false;
    private int currentPage = 0; // Nuvarande sida, b�rja med f�rsta sidan (index 0)

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            questionMark.SetActive(false);
            OpenBook();
        }

        if (bookPages[currentPage].activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseBook();
        }

        // Kontroll f�r att bl�ddra mellan sidor
        if (bookPages[currentPage].activeSelf && Input.GetKeyDown(KeyCode.RightArrow))
        {
            TurnPage(1); // N�sta sida
        }
        else if (bookPages[currentPage].activeSelf && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TurnPage(-1); // F�reg�ende sida
        }
    }

    void OpenBook()
    {
        PlayerController.Instance.CanMove = false;
        bookPages[currentPage].SetActive(true);
        hasReadBook = true;
    }

    void CloseBook()
    {
        bookPages[currentPage].SetActive(false);
        PlayerController.Instance.CanMove = true;
    }

    void TurnPage(int direction)
    {
        // St�ng nuvarande sida
        bookPages[currentPage].SetActive(false);

        // Ber�kna index f�r n�sta sida, och s�kerst�ll att det inte g�r utanf�r gr�nserna
        currentPage = Mathf.Clamp(currentPage + direction, 0, bookPages.Length - 1);

        // �ppna n�sta sida
        bookPages[currentPage].SetActive(true);
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